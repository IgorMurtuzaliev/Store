using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyStore.Models;
using System;
using MailKit;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

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
                    // наш email с заголовком письма
                    MailAddress from = new MailAddress("ingwarrior.99@yandex.ru", "Web Registration");
                    // кому отправляем
                    MailAddress to = new MailAddress(user.Email);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Email confirmation";
                    // текст письма - включаем в него ссылку
                    m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                    "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                        Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера, с которого мы и будем отправлять письмо
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 25);
                    // логин и пароль
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("ingwarrior.99@yandex.ru", "038161401IngWar9991");
                    smtp.Send(m);
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
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
        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            User user = this.UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Login", "Account", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
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
                    if (user.EmailConfirmed == true)
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
                    else
                    {
                        ModelState.AddModelError("", "Не подтвержден email.");
                    }
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback",
                    new { returnUrl = returnUrl })
            };

            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }
        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            User user = await UserManager.FindAsync(loginInfo.Login);

            if (user == null)
            {
                user = new User
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                    Name = loginInfo.Email,
                    LastName = loginInfo.Email
                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                else
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
            }

            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            ident.AddClaims(loginInfo.ExternalIdentity.Claims);

            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);

            return Redirect(returnUrl ?? "/");
        }

    }

}