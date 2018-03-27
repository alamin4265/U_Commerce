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
    public class LoginHistoryController : Controller
    {
        private MyCon db = new MyCon();

        // GET: LoginHistory
        public ActionResult Index()
        {
            var loginHistories = db.LoginHistories.Include(l => l.User);
            return View(loginHistories.ToList());
        }

        // GET: LoginHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginHistory loginHistory = db.LoginHistories.Find(id);
            if (loginHistory == null)
            {
                return HttpNotFound();
            }
            return View(loginHistory);
        }

        // GET: LoginHistory/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: LoginHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,DateTime,Ip")] LoginHistory loginHistory)
        {
            if (ModelState.IsValid)
            {
                db.LoginHistories.Add(loginHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", loginHistory.UserId);
            return View(loginHistory);
        }

        // GET: LoginHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginHistory loginHistory = db.LoginHistories.Find(id);
            if (loginHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", loginHistory.UserId);
            return View(loginHistory);
        }

        // POST: LoginHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,DateTime,Ip")] LoginHistory loginHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", loginHistory.UserId);
            return View(loginHistory);
        }

        // GET: LoginHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginHistory loginHistory = db.LoginHistories.Find(id);
            if (loginHistory == null)
            {
                return HttpNotFound();
            }
            return View(loginHistory);
        }

        // POST: LoginHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginHistory loginHistory = db.LoginHistories.Find(id);
            db.LoginHistories.Remove(loginHistory);
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
