using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SchoolBankProject.Data;
using SchoolBankProject.Data.AppliationUser;
using SchoolBankProject.Identity.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolBankProject.Identity
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(SchoolBankContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Identity/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}