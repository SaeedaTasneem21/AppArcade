using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class user_infoController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();

        // GET: user_info
        public ActionResult Index()
        {
            return View(db.user_info.ToList());
        }

        // GET: user_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_info user_info = db.user_info.Find(id);
            if (user_info == null)
            {
                return HttpNotFound();
            }
            return View(user_info);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user_info user_Info)
        {
            var user = db.user_info.SingleOrDefault(u => u.Email == user_Info.Email && u.UserPassword == user_Info.UserPassword);

            if (user != null)
            {
                Session["SessionId"] = user.UserId;
                ViewBag.Username = user.Username;
                return RedirectToAction("Index","Home");
                

            }
            else 
            { 
                return HttpNotFound(); 
            };
            //return View();
        }

        // GET: user_info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Email,UserPassword,PhoneNumber")] user_info user_info)
        {
            if (ModelState.IsValid)
            {
                db.user_info.Add(user_info);
                db.SaveChanges();
                SendNotification(user_info);

                Console.WriteLine("Email sent successfully");
                return RedirectToAction("Login", "user_info");
            }                          

            return View(user_info);
        }

        // GET: user_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_info user_info = db.user_info.Find(id);
            if (user_info == null)
            {
                return HttpNotFound();
            }
            return View(user_info);
        }

        // POST: user_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Email,UserPassword,PhoneNumber")] user_info user_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_info);
        }

        // GET: user_info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_info user_info = db.user_info.Find(id);
            if (user_info == null)
            {
                return HttpNotFound();
            }
            return View(user_info);
        }

        // POST: user_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_info user_info = db.user_info.Find(id);
            db.user_info.Remove(user_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult User_Panel(user_info user_Info)
        {
            int id = Convert.ToInt32(Session["SessionId"]);
            return View(db.user_info.Where(t => t.UserId == id));
           
        }

        public void SendNotification(user_info user_Info)
        { 
            var mail = new MailMessage();
            mail.From = new MailAddress("ashiquzzaman.joy99@gmail.com");
            mail.To.Add(user_Info.Email);

            mail.Subject = "Registration successfull";
            mail.Body = "Welcome";

            var smtp = new SmtpClient("smtp.gmail.com");

            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("ashiquzzaman.joy99@gmail.com", "mecdgdedswfdwtgg");

            smtp.Send(mail);
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
