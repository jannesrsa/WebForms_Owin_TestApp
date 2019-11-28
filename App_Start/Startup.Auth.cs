using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Owin;

namespace WebForms_Owin_TestApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            IAuthenticationSessionStore authenticationSessionStore = null;
            TicketDataFormat cookieTicketDataFormat = null;
            ICookieAuthenticationProvider authenticationProvider = null;

            app.Use(async (Context, next) =>
            {
                await next.Invoke(); //temp for debugging put break point here
            });

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

            // Add Application Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                LogoutPath = new PathString("/account/logout"),
                CookieName = DefaultAuthenticationTypes.ApplicationCookie,
                CookieHttpOnly = true,
                SlidingExpiration = true,
                SessionStore = authenticationSessionStore,
                TicketDataFormat = cookieTicketDataFormat,
                Provider = authenticationProvider,
            });

            // Add External Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                AuthenticationMode = AuthenticationMode.Passive,
                CookieName = DefaultAuthenticationTypes.ExternalCookie,
                CookieHttpOnly = true,
                SessionStore = authenticationSessionStore,
                TicketDataFormat = cookieTicketDataFormat,
                ExpireTimeSpan = TimeSpan.FromMinutes(5),
            });

            app.UseStageMarker(PipelineStage.MapHandler);

            app.Use(async (Context, next) =>
            {
                await next.Invoke(); //temp for debugging put break point here
            });

            app.UseStageMarker(PipelineStage.Authenticate);

            app.Use(async (Context, next) =>
            {
                await next.Invoke(); //temp for debugging put break point here
            });
        }
    }
}