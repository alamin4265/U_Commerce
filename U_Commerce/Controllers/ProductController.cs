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
    public class ProductController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductBrand).Include(p => p.ProductCategory).Include(p => p.ProductCondition).Include(p => p.User);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.ProductBrands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            ViewBag.ConditionId = new SelectList(db.ProductConditions, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            product.CreateDate = DateTime.Now;
            product.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.ProductBrands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);
            ViewBag.ConditionId = new SelectList(db.ProductConditions, "Id", "Name", product.ConditionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", product.UserId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.ProductBrands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);
            ViewBag.ConditionId = new SelectList(db.ProductConditions, "Id", "Name", product.ConditionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", product.UserId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            product.CreateDate = DateTime.Now;
            product.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.ProductBrands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);
            ViewBag.ConditionId = new SelectList(db.ProductConditions, "Id", "Name", product.ConditionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", product.UserId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
