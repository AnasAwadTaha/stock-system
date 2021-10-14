using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmacySysyem.DAL;
using PharmacySysyem.Models;

namespace PharmacySysyem.Controllers
{
    public class PurchaseInvoiceMController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: PurchaseInvoiceM
        public ActionResult Index()
        {
            return View(db.PurInvMs.ToList());
        }

        // GET: PurchaseInvoiceM/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseInvoiceM purchaseInvoiceM = db.PurInvMs.Find(id);
            if (purchaseInvoiceM == null)
            {
                return HttpNotFound();
            }
            return View(purchaseInvoiceM);
        }

        // GET: PurchaseInvoiceM/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return View();
        }
        //Get QTY By ItemID Stock And UnitID 
        public JsonResult GetItemQtyById(int ItemID, int UnitID, int StockID)
        {
            var ItemQty = (from i in db.Transactions
                           where (i.ItemCardMasterID == ItemID && i.UnitID == UnitID && i.StockID == StockID
                           && (i.Transactiontype == 1 || i.Transactiontype == 2))
                           select (int?)i.Qty).Sum() ?? 0;
            return Json(ItemQty, JsonRequestBehavior.AllowGet);
        }
        // POST: PurchaseInvoiceM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create([Bind(Include = "PurchaseDate,SupplierID,SupplierName,Sum,Discount,NetValue,Remark")] PurchaseInvoiceM purchaseInvoiceM, PurchaseInvoiceD[] PurchaseDet)
        {
            string result = "Error! Data Is Not Complete!";
            if (ModelState.IsValid)
            {
                var MasterPurchase = new PurchaseInvoiceM();
                MasterPurchase.PurchaseDate = purchaseInvoiceM.PurchaseDate;
                MasterPurchase.SupplierID = purchaseInvoiceM.SupplierID;
                MasterPurchase.Sum = purchaseInvoiceM.Sum;
                MasterPurchase.Discount = purchaseInvoiceM.Discount;
                MasterPurchase.NetValue = purchaseInvoiceM.NetValue;
                MasterPurchase.Remark = "Empty";
                db.PurInvMs.Add(purchaseInvoiceM);
                db.SaveChanges();
                long MasterPurchasID = purchaseInvoiceM.ID;
                foreach (var item in PurchaseDet)
                {
                    var tra = new Transaction();
                    var DetalisPurchase = new PurchaseInvoiceD();
                    DetalisPurchase.ItemCardMasterID = item.ItemCardMasterID;
                    DetalisPurchase.UnitID = item.UnitID;
                    DetalisPurchase.Qty = Convert.ToInt32(Services.ServiceClass.ConvertToSmallUnit(item.ItemCardMasterID, item.UnitID, item.Qty));
                    DetalisPurchase.Price = item.Price;
                    DetalisPurchase.TotalPrice = item.TotalPrice;
                    DetalisPurchase.StockID = item.StockID;
                    DetalisPurchase.DiscountItem = item.DiscountItem;
                    DetalisPurchase.PurchaseInvoiceMID = MasterPurchasID;
                    db.PurInvDs.Add(DetalisPurchase);
                    //Add To Transaction 
                    tra.StockID = item.StockID;
                    tra.Transactiontype = 2;
                    tra.TransactionDate = MasterPurchase.PurchaseDate;
                    tra.Qty = Convert.ToInt32(Services.ServiceClass.ConvertToSmallUnit(item.ItemCardMasterID, item.UnitID, item.Qty));
                    tra.ItemCardMasterID = Convert.ToInt32(item.ItemCardMasterID);
                    tra.PatchNo = 1;
                    tra.UnitID = item.UnitID;
                    tra.RefID = Convert.ToInt32(MasterPurchasID);
                    db.Transactions.Add(tra);
                }
                db.SaveChanges();
                result = "Success! Purchase Invoice Is completed!";
            }
            ViewBag.ItemID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PurchaseInvoiceM/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseInvoiceM purchaseInvoiceM = db.PurInvMs.Find(id);
            if (purchaseInvoiceM == null)
            {
                return HttpNotFound();
            }
            return View(purchaseInvoiceM);
        }

        // POST: PurchaseInvoiceM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PurchaseDate,SupplierID,SupplierName,Sum,Discount,NetValue,Remark")] PurchaseInvoiceM purchaseInvoiceM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseInvoiceM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseInvoiceM);
        }

        // GET: PurchaseInvoiceM/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseInvoiceM purchaseInvoiceM = db.PurInvMs.Find(id);
            if (purchaseInvoiceM == null)
            {
                return HttpNotFound();
            }
            return View(purchaseInvoiceM);
        }

        // POST: PurchaseInvoiceM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchaseInvoiceM purchaseInvoiceM = db.PurInvMs.Find(id);
            db.PurInvMs.Remove(purchaseInvoiceM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
