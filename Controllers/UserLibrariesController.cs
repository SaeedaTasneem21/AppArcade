using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class UserLibrariesController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();

        // GET: UserLibraries
        public ActionResult Index()
        {
            int? id = Session["SessionId"] as int ?;
            var userLibraries = db.UserLibraries.Include(u => u.Product_info).Include(u => u.user_info);
            return View(userLibraries.Where(t => t.USERID ==id ).ToList());            
        }
    
        public ActionResult Checkout()
        {
            int? userid = Session["SessionID"] as int?;
            if (Session["cart"] != null)
            {

                foreach (var item in (List<AppArcade.Models.Cart>)Session["cart"])
                {
                    var userLibraryItem = new UserLibrary
                    {
                        USERID = userid,
                        PRODUCTID = item.product.Product_id,
                        PDate = DateTime.Now,

                    };


                    db.UserLibraries.Add(userLibraryItem);
                }
                db.SaveChanges();
                Session["cart"] = null;


                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.PRODUCTID = new SelectList(db.Product_info, "Product_id", "Product_Name", userLibrary.PRODUCTID);
                //ViewBag.USERID = new SelectList(db.user_info, "UserId", "Username", userLibrary.USERID);
                //return View(userLibrary);
                return View();
            }

        }

        public ActionResult DownloadFile(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {                
                string contentType = "image/png";

                return File(FilePath, contentType );
            }
            else
            {               
                return HttpNotFound("File not found.");
            }
        }



        // GET: UserLibraries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLibrary userLibrary = db.UserLibraries.Find(id);
            if (userLibrary == null)
            {
                return HttpNotFound();
            }
            return View(userLibrary);
        }

        // GET: UserLibraries/Create
        public ActionResult Create()
        {
            ViewBag.PRODUCTID = new SelectList(db.Product_info, "Product_id", "Product_Name");
            ViewBag.USERID = new SelectList(db.user_info, "UserId", "Username");
            return View();
        }

        // POST: UserLibraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibraryId,USERID,PRODUCTID,PDate")] UserLibrary userLibrary)
        {
            if (ModelState.IsValid)
            {
                db.UserLibraries.Add(userLibrary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PRODUCTID = new SelectList(db.Product_info, "Product_id", "Product_Name", userLibrary.PRODUCTID);
            ViewBag.USERID = new SelectList(db.user_info, "UserId", "Username", userLibrary.USERID);
            return View(userLibrary);
        }

        // GET: UserLibraries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLibrary userLibrary = db.UserLibraries.Find(id);
            if (userLibrary == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCTID = new SelectList(db.Product_info, "Product_id", "Product_Name", userLibrary.PRODUCTID);
            ViewBag.USERID = new SelectList(db.user_info, "UserId", "Username", userLibrary.USERID);
            return View(userLibrary);
        }

        // POST: UserLibraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibraryId,USERID,PRODUCTID,PDate")] UserLibrary userLibrary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLibrary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PRODUCTID = new SelectList(db.Product_info, "Product_id", "Product_Name", userLibrary.PRODUCTID);
            ViewBag.USERID = new SelectList(db.user_info, "UserId", "Username", userLibrary.USERID);
            return View(userLibrary);
        }

        // GET: UserLibraries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLibrary userLibrary = db.UserLibraries.Find(id);
            if (userLibrary == null)
            {
                return HttpNotFound();
            }
            return View(userLibrary);
        }

        // POST: UserLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLibrary userLibrary = db.UserLibraries.Find(id);
            db.UserLibraries.Remove(userLibrary);
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
