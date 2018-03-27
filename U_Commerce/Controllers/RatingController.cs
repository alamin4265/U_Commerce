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
    public class RatingController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Rating
        public ActionResult Index()
        {
            var productRatings = db.ProductRatings.Include(p => p.Product).Include(p => p.User);
            return View(productRatings.ToList());
        }

        // GET: Rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            return View(productRating);
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,UserId,DateTime,Ip,Rating")] ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                db.ProductRatings.Add(productRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,UserId,DateTime,Ip,Rating")] ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productRating.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productRating.UserId);
            return View(productRating);
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductRating productRating = db.ProductRatings.Find(id);
            if (productRating == null)
            {
                return HttpNotFound();
            }
            return View(productRating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductRating productRating = db.ProductRatings.Find(id);
            db.ProductRatings.Remove(productRating);
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
