using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class ChatsController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();

        // GET: Chats/Create
        public ActionResult Create()
        {
            return View(db.Chats.ToList());
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chat chat)
        {            
            int? id = Session["SessionId"] as int?;
            if (Session["SessionId"]!=null || Session["ASessionId"]!=null)
            { 
                chat.UserId = id;
                chat.Timestamp = DateTime.UtcNow;
                db.Chats.Add(chat);
                db.SaveChanges();                
                
            }
            return View(db.Chats.ToList());
            
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
