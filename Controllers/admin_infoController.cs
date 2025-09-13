using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class admin_infoController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();

        // GET: admin_info
        public ActionResult Index()
        {
            return View(db.admin_info.ToList());
        }

        // GET: admin_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_info admin_info = db.admin_info.Find(id);
            if (admin_info == null)
            {
                return HttpNotFound();
            }
            return View(admin_info);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(admin_info Admin_Info)
        {
            var admin = db.admin_info.SingleOrDefault(a => a.AdminEmail == Admin_Info.AdminEmail && a.AdminPassword == Admin_Info.AdminPassword);

            if (admin != null)
            {
                Session["ASessionId"] = admin.AdminName;
                return RedirectToAction("Index", "admin_info");
                Response.Write("Good job");
            }
            else
            {
                return HttpNotFound();
            };
            return View();
        }

        // GET: admin_info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,AdminName,AdminPassword,AdminEmail")] admin_info admin_info)
        {
            if (ModelState.IsValid)
            {
                db.admin_info.Add(admin_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_info);
        }

        // GET: admin_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_info admin_info = db.admin_info.Find(id);
            if (admin_info == null)
            {
                return HttpNotFound();
            }
            return View(admin_info);
        }

        // POST: admin_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,AdminName,AdminPassword,AdminEmail")] admin_info admin_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_info);
        }

        // GET: admin_info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin_info admin_info = db.admin_info.Find(id);
            if (admin_info == null)
            {
                return HttpNotFound();
            }
            return View(admin_info);
        }

        // POST: admin_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            admin_info admin_info = db.admin_info.Find(id);
            db.admin_info.Remove(admin_info);
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
