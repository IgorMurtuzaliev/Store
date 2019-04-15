using MyStore.Areas.Admin.Models;
using MyStore.Models;
using MyStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(new CartIndexViewModel {
                Cart = GetCart()
            });
        }
        public ActionResult AddToCart(int?id)
        {   Product product = db.Products.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int? id)
        {
            Product product = db.Products.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index");
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}