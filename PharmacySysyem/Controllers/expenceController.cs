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
    public class expenceController : Controller
    {
        private PharmacyContext db = new PharmacyContext();

        // GET: expence
        public ActionResult Index()
        {
            return View(db.expences.ToList());
        }

        // GET: expence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expence expence = db.expences.Find(id);
            if (expence == null)
            {
                return HttpNotFound();
            }
            return View(expence);
        }

        // GET: expence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: expence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ExpenceName")] expence expence)
        {
            if (ModelState.IsValid)
            {
                db.expences.Add(expence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expence);
        }

        // GET: expence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expence expence = db.expences.Find(id);
            if (expence == null)
            {
                return HttpNotFound();
            }
            return View(expence);
        }

        // POST: expence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ExpenceName")] expence expence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expence);
        }

        // GET: expence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expence expence = db.expences.Find(id);
            if (expence == null)
            {
                return HttpNotFound();
            }
            return View(expence);
        }

        // POST: expence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            expence expence = db.expences.Find(id);
            db.expences.Remove(expence);
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
