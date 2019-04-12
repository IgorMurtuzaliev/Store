using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using log4net;
using Microsoft.AspNet.Identity;
using MyStore.Areas.Shop.Models;
using MyStore.Filters;
using MyStore.Models;
using MyStore.ViewModels;

namespace MyStore.Areas.Shop.Controllers
{

    public class OrdersController : Controller
    {
        private static readonly ILog log = log4net.LogManager.GetLogger("DBLog");
        private ApplicationDbContext db = new ApplicationDbContext();
        [MyAction]
        public ActionResult Index()
        {
            return View(db.Orders.Include(c => c.User).ToList());
        }
        [MyAction]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [MyAction]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel orderVM)
        {
            //[Bind(Include = "OrderId,TimeOfOrder,UserId")] 

            if (ModelState.IsValid)
            {
                var order = new Order
                { 
                    UserId = orderVM.UserId,
                    TimeOfOrder = DateTime.Now
                };


                var orderproduct = new ProductOrder
                {
                    ProductId = orderVM.ProductId,
                    Amount = orderVM.Amount,
                };

                order.ProductOrders.Add(orderproduct);


                db.Orders.Add(order);
                db.SaveChanges();

                //db.ProductOrders.Add(orderproduct);
                //db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(orderVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,TimeOfOrder,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            return View(order);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
