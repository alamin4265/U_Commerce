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
    public class BrandController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Brand
        public ActionResult Index()
        {
            return View(db.ProductBrands.ToList());
        }

        // GET: Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = db.ProductBrands.Find(id);
            if (productBrand == null)
            {
                return HttpNotFound();
            }
            return View(productBrand);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Origin")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                db.ProductBrands.Add(productBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productBrand);
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = db.ProductBrands.Find(id);
            if (productBrand == null)
            {
                return HttpNotFound();
            }
            return View(productBrand);
        }

        // POST: Brand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Origin")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productBrand);
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBrand productBrand = db.ProductBrands.Find(id);
            if (productBrand == null)
            {
                return HttpNotFound();
            }
            return View(productBrand);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBrand productBrand = db.ProductBrands.Find(id);
            db.ProductBrands.Remove(productBrand);
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
