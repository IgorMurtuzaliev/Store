using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FindData()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View(db.Orders.ToList());
        }
        [HttpPost]
        public ActionResult FindData(DateTime? from, DateTime? to, User user)
        {

            if (from != null || to != null)
            {
                var orders = db.Orders.Where(c => c.TimeOfOrder > from && c.TimeOfOrder < to).ToList();
                return View(orders);
            }
            if (user != null)
            {
                var orders = db.Orders.Where(c => c.User.Id == user.Id).ToList();
                return View(orders);
            }
            return View();


        }
    }
}