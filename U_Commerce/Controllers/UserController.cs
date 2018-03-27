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
    public class UserController : Controller
    {
        private MyCon db = new MyCon();


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == loginModel.Email.ToLower() && u.Password == loginModel.Password);
                if (user == null)
                {
                    ViewBag.Error = "Invalid Login";
                }
                else if(user.AdminUser==null)
                {
                    ViewBag.Error = "You have To Login With admin Account";
                }
                else
                {
                    Session["Id"] = user.Id;
                    Session["Name"] = user.Name;
                    Session["Type"] = "Admin";
                    if (Session["dv"] == null || Session["dv"].ToString() == "")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction(Session["dv"].ToString(),Session["dc"].ToString());
                    }
                }
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            Session["Id"] = "";
            Session["Name"] = "";
            Session["Type"] = "";
            return RedirectToAction("Login");
        }

        public ActionResult MyAccount()
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "MyAccount";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }
            return View();
        }
        
        public ActionResult Index()
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Index";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }

            var users = db.Users.Include(u => u.AdminUser).Include(u => u.City);
            return View(users.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Details";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Create";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }

            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId");
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                user.Image = System.IO.Path.GetFileName(Image.FileName);
            }
            user.JoinDate = DateTime.Now;
            user.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                if (Image != null)
                {
                    Image.SaveAs(Server.MapPath("../Uploads/UserImages/" + user.Id + "_" + Image.FileName));
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Edit";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase Image)
        {
            user.Ip = Request.UserHostAddress;
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    user.Image = System.IO.Path.GetFileName(Image.FileName);                   
                } 

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (Image != null)
                {
                    Image.SaveAs(Server.MapPath("~/Uploads/UserImages/" + user.Id + "_" + Image.FileName));
                }

                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AdminUsers, "UserId", "UserId", user.Id);
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Delete";
                Session["dc"] = "User";
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
