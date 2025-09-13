using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class HomeController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();
        public ActionResult Index()
        {
            return View(db.Product_info.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Remove("SessionId");
            return RedirectToAction("Login","user_info");            
        }
        [HttpPost]
        public ActionResult ALogout()
        {
            Session.Remove("ASessionId");
            return RedirectToAction("Login", "user_info");
        }
    }
}