using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using MyStore.Models;


[assembly: OwinStartup(typeof(MyStore.App_Start.Startup))]

namespace MyStore.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                AuthenticationType = "Google",
                ClientId = "405558759348-k91mpnp7r8vmmqsvcgii0uek2affej8b.apps.googleusercontent.com",
                ClientSecret = "SXtEzIDr3XJjj0uHzGpNU7Hv",
                Caption = "Авторизация через Google+",
                CallbackPath = new PathString("/GoogleLoginCallback"),
                BackchannelTimeout = TimeSpan.FromSeconds(60),
                BackchannelHttpHandler = new System.Net.Http.WebRequestHandler(),
                BackchannelCertificateValidator = null,
                Provider = new GoogleOAuth2AuthenticationProvider()
            });


        }
    }
}

