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
        public ActionResult Index(Cart cart)
        {
            return View(new CartIndexViewModel {
                Cart = cart
            });
        }
        public ActionResult AddToCart(Cart cart, int?id)
        {   Product product = db.Products.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { area="Shop", controller = "Products"});
        }

        public ActionResult RemoveFromCart(Cart cart, int? id)
        {
            Product product = db.Products.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}