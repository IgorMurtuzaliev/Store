using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyStore.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MyStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //public ActionResult Register()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register(Account account)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        XmlSerializer formatter = new XmlSerializer(typeof(Account));

        //        // получаем поток, куда будем записывать сериализованный объект
        //        using (FileStream fs = new FileStream(Server.MapPath("~/Files/register.xml"), FileMode.Append))
        //        {
        //            formatter.Serialize(fs, account);

        //            Console.WriteLine("Объект сериализован");
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        //public ActionResult Login()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(Account account)
        //{
        //    XmlSerializer formatter = new XmlSerializer(typeof(Account));

        //    // получаем поток, куда будем записывать сериализованный объект
        //    using (FileStream fs = new FileStream(Server.MapPath("~/Files/login.xml"), FileMode.Append))
        //    {
        //        formatter.Serialize(fs, account);

        //        Console.WriteLine("Объект сериализован");
        //    }
        //    return RedirectToAction("Index");

        //}
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Email, Email = model.Email, Name = model.Name, LastName = model.LastName };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }

}