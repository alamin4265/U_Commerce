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
    public class UserVerifiedController : Controller
    {
        private MyCon db = new MyCon();

        // GET: UserVerified
        public ActionResult Index()
        {
            var userVerifieds = db.UserVerifieds.Include(u => u.User);
            return View(userVerifieds.ToList());
        }

        // GET: UserVerified/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVerified userVerified = db.UserVerifieds.Find(id);
            if (userVerified == null)
            {
                return HttpNotFound();
            }
            return View(userVerified);
        }

        // GET: UserVerified/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users.Where(c=>c.UserVerified.UserId!=c.Id), "Id", "Name");
            return View();
        }

        // POST: UserVerified/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVerified userVerified)
        {
            userVerified.DateTime = DateTime.Now;
            userVerified.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.UserVerifieds.Add(userVerified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users.Where(c => c.UserVerified.UserId != c.Id), "Id", "Name");
            return View(userVerified);
        }

        // GET: UserVerified/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVerified userVerified = db.UserVerifieds.Find(id);
            if (userVerified == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userVerified.UserId);
            return View(userVerified);
        }

        // POST: UserVerified/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,DateTime,Ip")] UserVerified userVerified)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userVerified).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userVerified.UserId);
            return View(userVerified);
        }

        // GET: UserVerified/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVerified userVerified = db.UserVerifieds.Find(id);
            if (userVerified == null)
            {
                return HttpNotFound();
            }
            return View(userVerified);
        }

        // POST: UserVerified/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserVerified userVerified = db.UserVerifieds.Find(id);
            db.UserVerifieds.Remove(userVerified);
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
