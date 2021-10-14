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
    public class StockmodelController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: Stockmodel
        public ActionResult Index()
        {
            return View(db.Stockmodels.ToList());
        }

        // GET: Stockmodel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockmodel stockmodel = db.Stockmodels.Find(id);
            if (stockmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockmodel);
        }

        // GET: Stockmodel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stockmodel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stockId,StockName,StockPrice")] Stockmodel stockmodel)
        {
            if (ModelState.IsValid)
            {
                db.Stockmodels.Add(stockmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockmodel);
        }

        // GET: Stockmodel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockmodel stockmodel = db.Stockmodels.Find(id);
            if (stockmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockmodel);
        }

        // POST: Stockmodel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stockId,StockName,StockPrice")] Stockmodel stockmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockmodel);
        }

        // GET: Stockmodel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stockmodel stockmodel = db.Stockmodels.Find(id);
            if (stockmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockmodel);
        }

        // POST: Stockmodel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stockmodel stockmodel = db.Stockmodels.Find(id);
            db.Stockmodels.Remove(stockmodel);
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
