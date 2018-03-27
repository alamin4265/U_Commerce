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
    public class ProductVerifiedController : Controller
    {
        private MyCon db = new MyCon();

        // GET: ProductVerified
        public ActionResult Index()
        {
            var productVerifieds = db.ProductVerifieds.Include(p => p.AdminUser).Include(p => p.Product);
            return View(productVerifieds.ToList());
        }

        // GET: ProductVerified/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVerified productVerified = db.ProductVerifieds.Find(id);
            if (productVerified == null)
            {
                return HttpNotFound();
            }
            return View(productVerified);
        }

        // GET: ProductVerified/Create
        public ActionResult Create()
        {
            ViewBag.AdminUserId = new SelectList(db.Users.Where(c=>c.Id==c.AdminUser.UserId), "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products.Where(p=>p.Id!=p.ProductVerified.ProductId), "Id", "Name");
            return View();
        }

        // POST: ProductVerified/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,AdminUserId,DateTime,Ip")] ProductVerified productVerified)
        {
            productVerified.DateTime = DateTime.Now;
            productVerified.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.ProductVerifieds.Add(productVerified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminUserId = new SelectList(db.Users.Where(c => c.Id == c.AdminUser.UserId), "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products.Where(p => p.Id != p.ProductVerified.ProductId), "Id", "Name");
            return View(productVerified);
        }

        // GET: ProductVerified/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVerified productVerified = db.ProductVerifieds.Find(id);
            if (productVerified == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminUserId = new SelectList(db.Users.Where(c=>c.Id==c.AdminUser.UserId), "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productVerified.ProductId);
            return View(productVerified);
        }

        // POST: ProductVerified/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,AdminUserId,DateTime,Ip")] ProductVerified productVerified)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productVerified).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminUserId = new SelectList(db.AdminUsers, "UserId", "UserId", productVerified.AdminUserId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productVerified.ProductId);
            return View(productVerified);
        }

        // GET: ProductVerified/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVerified productVerified = db.ProductVerifieds.Find(id);
            if (productVerified == null)
            {
                return HttpNotFound();
            }
            return View(productVerified);
        }

        // POST: ProductVerified/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductVerified productVerified = db.ProductVerifieds.Find(id);
            db.ProductVerifieds.Remove(productVerified);
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
