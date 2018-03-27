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
    public class ProductClosedController : Controller
    {
        private MyCon db = new MyCon();

        // GET: ProductClosed
        public ActionResult Index()
        {
            var productCloseds = db.ProductCloseds.Include(p => p.Product).Include(p => p.ProductCloseType);
            return View(productCloseds.ToList());
        }

        // GET: ProductClosed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClosed productClosed = db.ProductCloseds.Find(id);
            if (productClosed == null)
            {
                return HttpNotFound();
            }
            return View(productClosed);
        }

        // GET: ProductClosed/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.ClosingTypeId = new SelectList(db.ProductCloseTypes, "Id", "Name");
            return View();
        }

        // POST: ProductClosed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,DateTime,ClosingTypeId")] ProductClosed productClosed)
        {
            productClosed.DateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ProductCloseds.Add(productClosed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productClosed.ProductId);
            ViewBag.ClosingTypeId = new SelectList(db.ProductCloseTypes, "Id", "Name", productClosed.ClosingTypeId);
            return View(productClosed);
        }

        // GET: ProductClosed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClosed productClosed = db.ProductCloseds.Find(id);
            if (productClosed == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productClosed.ProductId);
            ViewBag.ClosingTypeId = new SelectList(db.ProductCloseTypes, "Id", "Name", productClosed.ClosingTypeId);
            return View(productClosed);
        }

        // POST: ProductClosed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,DateTime,ClosingTypeId")] ProductClosed productClosed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productClosed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productClosed.ProductId);
            ViewBag.ClosingTypeId = new SelectList(db.ProductCloseTypes, "Id", "Name", productClosed.ClosingTypeId);
            return View(productClosed);
        }

        // GET: ProductClosed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductClosed productClosed = db.ProductCloseds.Find(id);
            if (productClosed == null)
            {
                return HttpNotFound();
            }
            return View(productClosed);
        }

        // POST: ProductClosed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductClosed productClosed = db.ProductCloseds.Find(id);
            db.ProductCloseds.Remove(productClosed);
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
