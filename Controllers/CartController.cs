using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppArcade.Models;

namespace AppArcade.Controllers
{
    public class CartController : Controller
    {
        private AppArcadeEntities db = new AppArcadeEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int productid)
        {


            if (Session["cart"] == null)
            {
                List<Cart> carts = new List<Cart>();

                carts.Add(new Cart() { product = db.Product_info.Find(productid) });
                Session["cart"] = carts;

            }
            else
            {
                List<Cart> carts = (List<Cart>) Session["cart"];
                int Index = IsInCart(productid);
                if (Index != -1)
                {
                    return View();
                }
                else
                {
                    carts.Add(new Cart() { product = db.Product_info.Find(productid) });
                }
                Session["cart"] = carts;
            }
            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromCart(int productid) 
        {
            List<Cart> carts = (List<Cart>)Session["cart"];
            int Index = IsInCart(productid);
            carts.RemoveAt(Index);
            Session["cart"] = carts;
            return RedirectToAction("Index");
        }
        public int IsInCart(int ProductId)
        {
            List<Cart> carts = (List<Cart>)Session["cart"];
            for (int i = 0; i < carts.Count; i++)
            {
                if (carts[i].product.Product_id == ProductId)
                    return i;
            }
            return -1;
        }

        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(String CCV)
        {
            
            Random rnd = new Random();
            int transactionResult = rnd.Next(0, 2);

            if (transactionResult == 1)
            {
                ViewBag.Message = "Payment Successful!";
                
            }
            else
            {
                ViewBag.Message = "Payment Failed. Please try again.";
            }

            return RedirectToAction("Checkout","UserLibraries");
        }

    }
}