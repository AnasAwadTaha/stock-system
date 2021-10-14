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
using PharmacySysyem.ViewModel;
using PagedList;

namespace PharmacySysyem.Controllers
{
    public class ItemCardMasterController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: ItemCardMaster
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            IQueryable<ItemCardVM> ItemCardVMlist = from master in db.ItemCardMasters
                                                    join ClassName in db.Classifications on master.ClassificationID equals ClassName.ClassificationID
                                                    where master.ClassificationID == ClassName.ClassificationID
                                                    select new ItemCardVM()
                                                    {
                                                        ItemCardMasterID = master.ItemCardMasterID,
                                                        ItemName = master.ItemName,
                                                        ClassificationName = ClassName.ClassificationName
                                                    };
            if (!String.IsNullOrEmpty(searchString))
            {
                ItemCardVMlist = ItemCardVMlist.Where(s => s.ItemName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ItemCardVMlist = ItemCardVMlist.OrderByDescending(n => n.ItemName);
                    break;
                default:
                    ItemCardVMlist = ItemCardVMlist.OrderBy(n => n.ItemName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(ItemCardVMlist.ToPagedList(pageNumber, pageSize));
        }
        //Validate CheckUser
        [HttpPost]
        public JsonResult doesItemNameExist(string ItemName)
        {
            bool result = false;
            var It = db.ItemCardMasters.Where(i => i.ItemName == ItemName).FirstOrDefault();
            if (It != null ? result = true : false)
            {
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: ItemCardMaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCardMaster itemCardMaster = db.ItemCardMasters.Find(id);
            if (itemCardMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemCardMaster);
        }
        //Save MasterItem Parameter ItemCardMasterID, ItemName, CountryID, SupplierID, ClassificationID, CommName, Status
        public ActionResult SaveItem(string name, int SupplierID, string Barcode, int CountryID, int ClassificationID, string CommName, ItemCardDetalis[] Details/*, HttpPostedFileBase file*/)
        {
            string result = "Error! Data Is Not Complete!";
            if (name != null && Details != null)
            {
                //Save Master Item card
                ItemCardMaster master = new ItemCardMaster();
                master.ItemName = name;
                master.SupplierID = SupplierID;
                master.CountryID = CountryID;
                master.ClassificationID = ClassificationID;
                master.CommName = CommName;
                master.Barcode = Barcode;
                //file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/ItemImages/")
                //                                         + file.FileName);
                //  master.Image = file.FileName;
                db.ItemCardMasters.Add(master);
                db.SaveChanges();
                //Get masterID after Save To use in details for loop
                long MasterID = master.ItemCardMasterID;
                //Save Master Item card Details
                foreach (var item in Details)
                {
                    ItemCardDetalis Detalis = new ItemCardDetalis();
                    Detalis.UnitID = item.UnitID;
                    Detalis.Package = item.Package;
                    Detalis.CostPrice = item.CostPrice;
                    Detalis.CurrntPrice = item.CurrntPrice;
                    Detalis.AvrgeCost = item.AvrgeCost;
                    Detalis.SalesPrice = item.SalesPrice;
                    //Detalis.Distrbut = item.Distrbut;
                    //Detalis.CostPriceAll = item.CostPriceAll;
                    Detalis.ItemCardMasterID = MasterID;
                    db.ItemCardDetaliss.Add(Detalis);
                }
                db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        // GET: ItemCardMaster/Create
        public ActionResult Create()
        {
            ViewBag.ClassificationID = new SelectList(db.Classifications, "ClassificationID", "ClassificationName");
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return View();
        }

        // POST: ItemCardMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemCardMasterID,ItemName,CountryID,SupplierID,ClassificationID,CommName,Status")] ItemCardMaster itemCardMaster)
        {
            if (ModelState.IsValid)
            {
                db.ItemCardMasters.Add(itemCardMaster);
                db.SaveChanges();
                var MasterID = itemCardMaster.ItemCardMasterID;
                return RedirectToAction("Index");
            }
            ViewBag.ClassificationID = new SelectList(db.Classifications, "ClassificationID", "ClassificationName", itemCardMaster.ClassificationID);
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Name", itemCardMaster.CountryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", itemCardMaster.SupplierID);
            //   ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName", itemCardDetalis.UnitID);
            return View(itemCardMaster);
        }
        public  JsonResult GetDetalis(int? id)
        {
            var itemDet = db.ItemCardDetaliss.Where(i => i.ItemCardMasterID == 56).ToList();
            return Json(itemDet,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itemCardMaster = db.ItemCardMasters
                .Include(d => d.ItemCardDetaliss)
                .Where(i => i.ItemCardMasterID == id)
                .Single();
            if (itemCardMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.Detalis = itemCardMaster.ItemCardDetaliss.ToList();
           // ViewBag.itemCardMaster = itemCardMaster;
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            ViewBag.ClassificationID = new SelectList(db.Classifications, "ClassificationID", "ClassificationName", itemCardMaster.ClassificationID);
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Name", itemCardMaster.CountryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", itemCardMaster.SupplierID);
            return View(itemCardMaster);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ItemCardMasterID,ItemName,CountryID,SupplierID,ClassificationID,CommName,Barcode,Status")]ItemCardMaster itemCardMaster,ItemCardDetalis[] det)
        {
            string result = "Error! Data Is Not Complete Edit!";
            if (ModelState.IsValid)
            {
                db.Entry(itemCardMaster).State = EntityState.Modified;
                db.ItemCardDetaliss.RemoveRange(db.ItemCardDetaliss.Where(i => i.ItemCardMasterID == itemCardMaster.ItemCardMasterID).ToList());
                // ItemCard Details
                foreach (var item in det)
                {
                    var Detalis = new ItemCardDetalis();
                    Detalis.UnitID = item.UnitID;
                    Detalis.Package = item.Package;
                    Detalis.CostPrice = item.CostPrice;
                    Detalis.CurrntPrice = item.CurrntPrice;
                    Detalis.AvrgeCost = item.AvrgeCost;
                    Detalis.SalesPrice = item.SalesPrice;
                    //Detalis.Distrbut = item.Distrbut;
                    //Detalis.CostPriceAll = item.CostPriceAll;
                    Detalis.ItemCardMasterID = itemCardMaster.ItemCardMasterID;
                    db.ItemCardDetaliss.Add(Detalis);
                }
                db.SaveChanges();
            }
            result = "Success! Item Edit Is Complete!";
            ViewBag.ClassificationID = new SelectList(db.Classifications, "ClassificationID", "ClassificationName", itemCardMaster.ClassificationID);
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Name", itemCardMaster.CountryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", itemCardMaster.SupplierID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: ItemCardMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCardMaster itemCardMaster = db.ItemCardMasters.Find(id);
            if (itemCardMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemCardMaster);
        }

        // POST: ItemCardMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCardMaster itemCardMaster = db.ItemCardMasters.Find(id);
            db.ItemCardMasters.Remove(itemCardMaster);
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
