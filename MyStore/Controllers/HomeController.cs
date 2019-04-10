using log4net;
using MyStore.Filters;
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
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        private ApplicationDbContext db = new ApplicationDbContext();
        Exception ex = new Exception();
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
            return View();
            //if (user != null)
            //{
            //    var orders = db.Orders.Where(c => c.User.Id == user.Id).ToList();
            //    return View(orders);
            //}          
        }

        public string LoggerTest(int id)
        {
            if (id != 11)
            {
                return "norm";
            }
            else
            {
                Logger.Info("Testing information log");
                Logger.Debug("Testing Debug log");
                Logger.Fatal("Testing Fatal log");
                Logger.Error(ex);
                return "Error";
            }
        }
    }
}