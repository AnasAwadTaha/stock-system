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
    public class ReceiptPurchaseMController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: ReceiptPurchaseM
        public ActionResult Index()
        {
            return View(db.RecPurM.ToList());
        }
        //Get : PurchaseOrder index
        public ActionResult PurchaseOrderIndex(string sortOrder, string currentFilter, string searchString, int? page)
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
            var item = from i in db.PurOrM
                       where i.Confirmed == false
                       select i;
            // var PurchaseOrderList = (db.PurOrM.Where(s => s.Confirmed == false)).ToList();

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

        // GET: ReceiptPurchaseM/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptPurchaseM receiptPurchaseM = db.RecPurM.Find(id);
            if (receiptPurchaseM == null)
            {
                return HttpNotFound();
            }
            return View(receiptPurchaseM);
        }

        // GET: ReceiptPurchaseM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceiptPurchaseM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ReceiptDate,PurchaseOrderMsID,SupplierID,SupplierName,Sum,Discount,NetValue,Remark,Confirmed")] ReceiptPurchaseM receiptPurchaseM)
        {
            if (ModelState.IsValid)
            {
                db.RecPurM.Add(receiptPurchaseM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receiptPurchaseM);
        }
        // GET: ReceiptPurchaseM/Edit/5
        public ActionResult Receipt(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderM purchaseOrderM = db.PurOrM.Include(d => d.PurOrD).Where(I => I.ID == id).SingleOrDefault();

            ReceiptPurchaseM receiptPurchaseM = db.RecPurM.Find(id);
            if (receiptPurchaseM == null)
            {
                return HttpNotFound();
            }
            return View(receiptPurchaseM);
        }

        // GET: ReceiptPurchaseM/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptPurchaseM receiptPurchaseM = db.RecPurM.Find(id);
            if (receiptPurchaseM == null)
            {
                return HttpNotFound();
            }
            return View(receiptPurchaseM);
        }

        // POST: ReceiptPurchaseM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ReceiptDate,PurchaseOrderMsID,SupplierID,SupplierName,Sum,Discount,NetValue,Remark,Confirmed")] ReceiptPurchaseM receiptPurchaseM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptPurchaseM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receiptPurchaseM);
        }

        // GET: ReceiptPurchaseM/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceiptPurchaseM receiptPurchaseM = db.RecPurM.Find(id);
            if (receiptPurchaseM == null)
            {
                return HttpNotFound();
            }
            return View(receiptPurchaseM);
        }

        // POST: ReceiptPurchaseM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ReceiptPurchaseM receiptPurchaseM = db.RecPurM.Find(id);
            db.RecPurM.Remove(receiptPurchaseM);
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
