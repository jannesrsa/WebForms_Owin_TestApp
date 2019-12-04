using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;
using System.IdentityModel.Tokens;
using WebForms_Owin_TestApp.Services;

namespace WebForms_Owin_TestApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                LoginPath = new PathString("/account/login"),
                LogoutPath = new PathString("/account/logout"),
            });

            var claimsService = new ClaimsService();

            foreach (var issuer in claimsService.Issuers)
            {
                var wsFederation = new WsFederationAuthenticationOptions
                {
                    AuthenticationType = issuer.IssuerName,
                    Caption = issuer.Name,
                    MetadataAddress = issuer.MetadataUrl,
                    Wtrealm = claimsService.CurrentRealm.RealmUri,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false
                    }
                };

                app.UseWsFederationAuthentication(wsFederation);
            }

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}