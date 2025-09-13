using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class Product_infoController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();

        // GET: Product_info
        public ActionResult Index( string searching, string category)
        {
            int? userid = Session["SessionId"] as int?;
            var Products = db.Product_info.AsQueryable();

            var libproduct = db.UserLibraries.Where(ul=> ul.USERID ==userid).Select(ul => ul.PRODUCTID).ToList();

            Products = Products.Where(p => !libproduct.Contains(p.Product_id));

            if (!string.IsNullOrEmpty(searching))
            {
                Products = Products.Where(x => x.Product_Name.StartsWith(searching) || searching == null);
            }
            if (!string.IsNullOrEmpty(category))
            {
                Products = Products.Where(x => x.Product_Category.StartsWith(category) || category == null);
            }
            
            return View(Products.ToList());
        }

     
       
        // GET: Product_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_info product_info = db.Product_info.Find(id);
            if (product_info == null)
            {
                return HttpNotFound();
            }
            return View(product_info);
        }

        // GET: Product_info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_id,Product_Name,Product_Image,Product_Price,Product_Category,Product_Description,Product_Data")] Product_info product_info)
        {
            if (ModelState.IsValid)
            {   
                db.Product_info.Add(product_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_info);
        }

        // GET: Product_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_info product_info = db.Product_info.Find(id);
            if (product_info == null)
            {
                return HttpNotFound();
            }
            return View(product_info);
        }

        // POST: Product_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id,Product_Name,Product_Image,Product_Price,Product_Category,Product_Description,Product_Data")] Product_info product_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_info);
        }

        // GET: Product_info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_info product_info = db.Product_info.Find(id);
            if (product_info == null)
            {
                return HttpNotFound();
            }
            return View(product_info);
        }

        // POST: Product_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_info product_info = db.Product_info.Find(id);
            db.Product_info.Remove(product_info);
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
