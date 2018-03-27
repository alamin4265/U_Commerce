using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using U_Commerce.Models;

namespace U_Commerce.Controllers
{
    public class ConditionController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Condition
        public ActionResult Index()
        {
            return View(db.ProductConditions.ToList());
        }

        // GET: Condition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCondition productCondition = db.ProductConditions.Find(id);
            if (productCondition == null)
            {
                return HttpNotFound();
            }
            return View(productCondition);
        }

        // GET: Condition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Condition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ProductCondition productCondition)
        {
            if (ModelState.IsValid)
            {
                db.ProductConditions.Add(productCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCondition);
        }

        // GET: Condition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCondition productCondition = db.ProductConditions.Find(id);
            if (productCondition == null)
            {
                return HttpNotFound();
            }
            return View(productCondition);
        }

        // POST: Condition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] ProductCondition productCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCondition);
        }

        // GET: Condition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCondition productCondition = db.ProductConditions.Find(id);
            if (productCondition == null)
            {
                return HttpNotFound();
            }
            return View(productCondition);
        }

        // POST: Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCondition productCondition = db.ProductConditions.Find(id);
            db.ProductConditions.Remove(productCondition);
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
