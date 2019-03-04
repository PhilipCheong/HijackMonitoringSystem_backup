using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
[assembly: OwinStartup(typeof(HijackMonitoringApplication.StartUp))]

namespace HijackMonitoringApplication
{
    public class StartUp
    { 
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app); // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            var cookieProvider = new CookieAuthenticationProvider
            {
                // ... Options from your existing application
            };
            // Modify redirect behaviour to convert login URL to relative
            var applyRedirect = cookieProvider.OnApplyRedirect;
            cookieProvider.OnApplyRedirect = context =>
            {
                if (context.RedirectUri.StartsWith("http://" + context.Request.Host))
                {
                    context.RedirectUri = context.RedirectUri.Substring(
                        context.RedirectUri.IndexOf('/', "http://".Length));
                }
                applyRedirect(context);
            };



            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = cookieProvider
            });

            // MAKE SURE PROVIDERS KEYS ARE SET IN the CodePasteKeys.json FILE
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;


            //Setup SignalR
            app.MapSignalR();
        }

    }
}