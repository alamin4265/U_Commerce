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
    public class CloseTypeController : Controller
    {
        private MyCon db = new MyCon();

        // GET: CloseType
        public ActionResult Index()
        {
            return View(db.ProductCloseTypes.ToList());
        }

        // GET: CloseType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCloseType productCloseType = db.ProductCloseTypes.Find(id);
            if (productCloseType == null)
            {
                return HttpNotFound();
            }
            return View(productCloseType);
        }

        // GET: CloseType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CloseType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ProductCloseType productCloseType)
        {
            if (ModelState.IsValid)
            {
                db.ProductCloseTypes.Add(productCloseType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCloseType);
        }

        // GET: CloseType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCloseType productCloseType = db.ProductCloseTypes.Find(id);
            if (productCloseType == null)
            {
                return HttpNotFound();
            }
            return View(productCloseType);
        }

        // POST: CloseType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] ProductCloseType productCloseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCloseType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCloseType);
        }

        // GET: CloseType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCloseType productCloseType = db.ProductCloseTypes.Find(id);
            if (productCloseType == null)
            {
                return HttpNotFound();
            }
            return View(productCloseType);
        }

        // POST: CloseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCloseType productCloseType = db.ProductCloseTypes.Find(id);
            db.ProductCloseTypes.Remove(productCloseType);
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
