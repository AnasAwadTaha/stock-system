using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using PharmacySysyem.DAL;
using PharmacySysyem.Models;

namespace PharmacySysyem.Controllers
{
    public class PurchaseOrderMController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: PurchaseOrderM
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var item = from i in db.PurOrM.Include(s=>s.Suppliers)
                       select i;
            
            if (!string.IsNullOrEmpty(searchString))
            {
                int PurchaseID = 0;
                try
                {
                    PurchaseID = int.Parse(searchString);
                  
                }

                catch (Exception)
                {
                   
                }
                item = item.Where(s => s.Suppliers.SupplierName.Contains(searchString) ||
                  s.ID == PurchaseID);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    item = item.OrderByDescending(s => s.Suppliers.SupplierName);
                    break;
                default:
                    item = item.OrderBy(s => s.Suppliers.SupplierName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(item.ToPagedList(pageNumber, pageSize));
        }

        // GET: PurchaseOrderM/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderM purchaseOrderM = db.PurOrM.Find(id);
            if (purchaseOrderM == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderM);
        }

        // GET: PurchaseOrderM/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return View();
        }

        // POST: PurchaseOrderM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create([Bind(Include = "SupplierID,OrderDate,Sum,Discount,NetValue,Remark,Confirmed")] PurchaseOrderM purchaseOrderM, PurchaseOrderD[] PurchaseDet)
        {
            string result = "Error! Data Is Not Complete!";
           // ModelState.Remove("ErrorKey");
            if (ModelState.IsValid)
            {
                //Save PurchaseOrderM To Get ID To Save In Detalis
                db.PurOrM.Add(purchaseOrderM);
                db.SaveChanges();
                //Get MasterID To save in Detalis
                long MasterID = purchaseOrderM.ID;
                foreach (var item in PurchaseDet)
                {
                    PurchaseOrderD detalis = new PurchaseOrderD();
                    detalis.ItemCardMasterID = item.ItemCardMasterID;
                    detalis.UnitID = item.UnitID;
                    detalis.Price = item.Price;
                    detalis.Qty = item.Qty;
                    detalis.TotalPrice = item.TotalPrice;
                    detalis.DiscountItem = item.DiscountItem;
                    detalis.PurchaseMasterID = MasterID;

                    db.PurOrD.Add(detalis);
                }
                db.SaveChanges();
                result = "Success! Purchase Order Is Complate!";
            }
            ViewBag.ItemID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: PurchaseOrderM/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderM purchaseOrderM = db.PurOrM.Find(id);
            if (purchaseOrderM == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", purchaseOrderM.SupplierID);
            return View(purchaseOrderM);
        }

        // POST: PurchaseOrderM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SupplierID,OrderDate,Sum,Discount,NetValue,Remark,Confirmed")] PurchaseOrderM purchaseOrderM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", purchaseOrderM.SupplierID);
            return View(purchaseOrderM);
        }
        public JsonResult GetItemPriceById(int ItemID, int UnitID)
        {
            decimal ItemPrice = db.ItemCardDetaliss.Where(i => i.ItemCardMasterID == ItemID && i.UnitID == UnitID).Select(p => p.SalesPrice).FirstOrDefault();
            return Json(ItemPrice, JsonRequestBehavior.AllowGet);
        }
        // GET: PurchaseOrderM/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderM purchaseOrderM = db.PurOrM.Find(id);
            if (purchaseOrderM == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderM);
        }

        // POST: PurchaseOrderM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchaseOrderM purchaseOrderM = db.PurOrM.Find(id);
            db.PurOrM.Remove(purchaseOrderM);
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
