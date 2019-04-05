using MyStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace MyStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Account));

                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(Server.MapPath("~/Files/register.xml"), FileMode.Append))
                {
                    formatter.Serialize(fs, account);

                    Console.WriteLine("Объект сериализован");
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Account));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(Server.MapPath("~/Files/login.xml"), FileMode.Append))
            {
                formatter.Serialize(fs, account);

                Console.WriteLine("Объект сериализован");
            }
            return RedirectToAction("Index");

        }
    }
}