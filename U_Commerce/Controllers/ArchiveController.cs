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
    public class ArchiveController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Archive
        public ActionResult Index()
        {
            var productArchives = db.ProductArchives.Include(p => p.Product).Include(p => p.User);
            return View(productArchives.ToList());
        }

        // GET: Archive/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductArchive productArchive = db.ProductArchives.Find(id);
            if (productArchive == null)
            {
                return HttpNotFound();
            }
            return View(productArchive);
        }

        // GET: Archive/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Archive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductArchive productArchive)
        {
            productArchive.DateTime = DateTime.Now;
            productArchive.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.ProductArchives.Add(productArchive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productArchive.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productArchive.UserId);
            return View(productArchive);
        }

        // GET: Archive/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductArchive productArchive = db.ProductArchives.Find(id);
            if (productArchive == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productArchive.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productArchive.UserId);
            return View(productArchive);
        }

        // POST: Archive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,UserId,DateTime,Ip")] ProductArchive productArchive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productArchive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productArchive.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productArchive.UserId);
            return View(productArchive);
        }

        // GET: Archive/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductArchive productArchive = db.ProductArchives.Find(id);
            if (productArchive == null)
            {
                return HttpNotFound();
            }
            return View(productArchive);
        }

        // POST: Archive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductArchive productArchive = db.ProductArchives.Find(id);
            db.ProductArchives.Remove(productArchive);
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
