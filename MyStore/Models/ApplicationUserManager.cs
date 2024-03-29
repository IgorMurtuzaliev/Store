﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MyStore.Models
{
    public class ApplicationUserManager: UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store): base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
                                                IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<User>(db));
            return manager;
        }

        internal static Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            throw new NotImplementedException();
        }
    }
}