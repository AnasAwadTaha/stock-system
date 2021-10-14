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
using PharmacySysyem.Services;

namespace PharmacySysyem.Controllers
{
    public class OpenningBalanceMController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: OpenningBalanceM
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
            var item = from i in db.OpBaM.Include(D => D.ItemCardMasters)
                       select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(N => N.ItemCardMasters.ItemName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    item = item.OrderByDescending(s => s.ItemCardMasters.ItemName);
                    break;
                default:
                    item = item.OrderBy(s => s.ItemCardMasters.ItemName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(item.ToPagedList(pageNumber, pageSize));
        }

        // GET: OpenningBalanceM/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenningBalanceM openningBalanceM = db.OpBaM.Find(id);
            if (openningBalanceM == null)
            {
                return HttpNotFound();
            }
            return View(openningBalanceM);
        }
        //Validate CheckUser
        [HttpPost]
        public JsonResult doesItemNameExist(int ItemID, int StockID, int UnitID)
        {
            bool result = false;
            var Item = (from i in db.OpBaM
                        join U in db.OpBaD on i.ID equals U.OpenningBalanceMID
                        where (i.ItemCardMasterID == ItemID && U.UnitID == UnitID && U.stockID == StockID)
                        select i.ID).Count();
            if (Item > 0 ? result = true : false) { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: OpenningBalanceM/Create
        public ActionResult Create()
        {
            ViewBag.ItemCardMasterID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return View();
        }

        // POST: OpenningBalanceM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Create(OpenningBalanceM openningBalanceM, OpenningBalanceD[] OpenningDetails)
        {
            string result = "Error! Data Is Not Complete!";
            if (ModelState.IsValid)
            {
                //Save OpenningBalanceM To Get ID To Save In Detalis
                OpenningBalanceM OpMasetr = new OpenningBalanceM();
                OpMasetr.ItemCardMasterID = openningBalanceM.ItemCardMasterID;
                OpMasetr.Date = openningBalanceM.Date;
                db.OpBaM.Add(OpMasetr);
                db.SaveChanges();
                // Get OpenningMasterID To Save In Detalis And use To Transaction passed By RefID 
                long OpenningMasterID = OpMasetr.ID;
                foreach (var item in OpenningDetails)
                {
                    Transaction tra = new Transaction();
                    OpenningBalanceD openningBalanceD = new OpenningBalanceD();
                    openningBalanceD.stockID = item.stockID;
                    openningBalanceD.UnitID = item.UnitID;
                    openningBalanceD.ExpertDate = item.ExpertDate;
                    openningBalanceD.PatchID = item.PatchID;
                    openningBalanceD.Qty = Convert.ToInt32(ServiceClass.ConvertToSmallUnit(openningBalanceM.ItemCardMasterID, item.UnitID, item.Qty));
                    openningBalanceD.OpenningBalanceMID = OpenningMasterID;
                    //Add To Transaction 
                    db.Transactions.RemoveRange(db.Transactions.Where(t => t.ItemCardMasterID == openningBalanceM.ItemCardMasterID && t.UnitID == item.UnitID && t.StockID == item.stockID && t.Transactiontype == 1));
                    tra.StockID = item.stockID;
                    tra.Transactiontype = 1;
                    tra.Qty = openningBalanceD.Qty;
                    tra.TransactionDate = OpMasetr.Date;
                    tra.ItemCardMasterID = Convert.ToInt32(OpMasetr.ItemCardMasterID);
                    tra.PatchNo = item.PatchID;
                    tra.UnitID = item.UnitID;
                    tra.RefID = Convert.ToInt32(OpenningMasterID);
                    db.OpBaD.Add(openningBalanceD);
                    db.Transactions.Add(tra);
                }
                db.SaveChanges();
                result = "Success! Openning Balance Is Saves!";
            }
            ViewBag.ItemCardMasterID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName", openningBalanceM.ItemCardMasterID);
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: OpenningBalanceM/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Get All Unit,package From itemcardDetalis And Qty Associated OpBaD
            var GetUnitItemD = (from D in db.OpBaD
                                join M in db.OpBaM on D.OpenningBalanceMID equals M.ID
                                where M.ID == id
                                join ItD in db.ItemCardDetaliss on M.ItemCardMasterID equals ItD.ItemCardMasterID
                                where ItD.UnitID == D.UnitID
                                select new { ItD.UnitID, ItD.Package, D.Qty });
            //
           // var Item = GetUnitItemD.Where(X => X.Equals(true)).ToList();

            //Egaer Loding Master With Detalis
            var openningBalanceM = db.OpBaM.Include(D => D.OpBaD).Where(M => M.ID == id).SingleOrDefault();
            if (openningBalanceM == null)
            {
                return HttpNotFound();
            }
            ViewBag.Detalis = openningBalanceM.OpBaD.ToList();
            ViewBag.ItemCardMasterID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName", openningBalanceM.ItemCardMasterID);
            ViewBag.UnitID = new SelectList(db.Units, "ID", "UnitName");
            ViewBag.StockID = new SelectList(db.Stocks, "ID", "StockName");
            return View(openningBalanceM);
        }

        // POST: OpenningBalanceM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit([Bind(Include = "ID,ItemCardMasterID,Date")] OpenningBalanceM openningBalanceM, OpenningBalanceD[] DetalisOJ)
        {
            string result = "Error! Data Is Not Complete Edit!";
            if (ModelState.IsValid)
            {
                db.Entry(openningBalanceM).State = EntityState.Modified;
                foreach (var item in DetalisOJ)
                {
                    db.Transactions.Remove(db.Transactions.Where(s => s.StockID == item.stockID && s.Transactiontype == 1 && s.ItemCardMasterID == openningBalanceM.ItemCardMasterID && s.UnitID == item.UnitID && s.RefID == openningBalanceM.ID).SingleOrDefault());
                    var Detalis = new OpenningBalanceD();
                    var trans = new Transaction();
                    Detalis.stockID = item.stockID;
                    Detalis.UnitID = item.UnitID;
                    Detalis.ExpertDate = item.ExpertDate;
                    Detalis.PatchID = item.PatchID;
                    Detalis.Qty = item.PatchID;
                    Detalis.OpenningBalanceMID = openningBalanceM.ID;
                    trans.StockID = item.stockID;
                    trans.Transactiontype = 1;
                    trans.Qty = item.Qty;
                    trans.ItemCardMasterID = Convert.ToInt32(openningBalanceM.ItemCardMasterID);
                    trans.PatchNo = item.PatchID;
                    trans.UnitID = item.UnitID;
                    trans.RefID = Convert.ToInt32(openningBalanceM.ID);
                    trans.TransactionDate = openningBalanceM.Date;
                    db.OpBaD.Add(Detalis);
                }
                db.SaveChanges();
            }
            result = "Success! Item Edit Is Complete!";
            ViewBag.ItemCardMasterID = new SelectList(db.ItemCardMasters, "ItemCardMasterID", "ItemName", openningBalanceM.ItemCardMasterID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: OpenningBalanceM/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenningBalanceM openningBalanceM = db.OpBaM.Find(id);
            if (openningBalanceM == null)
            {
                return HttpNotFound();
            }
            return View(openningBalanceM);
        }

        // POST: OpenningBalanceM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OpenningBalanceM openningBalanceM = db.OpBaM.Find(id);
            db.OpBaM.Remove(openningBalanceM);
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
