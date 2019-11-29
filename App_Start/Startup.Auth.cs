using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using SourceCode.Security.Claims;
using System;
using System.Linq;
using WebForms_Owin_TestApp.Helpers;

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

            var claimsServer = ConnectionHelper.GetServer<ClaimsManagement>();
            var _currentRealm = claimsServer.GetRealm("https://k2.denallix.com/WebForms_Owin_TestApp/");

            var realmPathBase = new Uri(_currentRealm.RealmUri).AbsolutePath;
            var issuers = _currentRealm.Issuers.Where(iss => !string.IsNullOrWhiteSpace(iss.MetadataUrl) && iss.UseForLogin);

            foreach (var issuer in issuers)
            {
                app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Passive,
                    AuthenticationType = issuer.Name,
                    Caption = issuer.Name,
                    CallbackPath = new PathString($"{realmPathBase.ToLower()}signin-wsfed{issuer.ID.ToString()}"),
                    MetadataAddress = issuer.MetadataUrl,
                    Wtrealm = _currentRealm.RealmUri,

                    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                    {
                        SaveSigninToken = true,
                    },

                    Notifications = new WsFederationAuthenticationNotifications()
                });
            }

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