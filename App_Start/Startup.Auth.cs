using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;
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
                AuthenticationType = WsFederationAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/account/login"),
                LogoutPath = new PathString("/account/logout"),
            });

            var claimsService = new ClaimsService();

            foreach (var issuer in claimsService.Issuers)
            {
                var wsFederation = new WsFederationAuthenticationOptions
                {
                    AuthenticationType = WsFederationAuthenticationDefaults.AuthenticationType,
                    Caption = issuer.Name,
                    MetadataAddress = issuer.MetadataUrl,
                    Wtrealm = claimsService.CurrentRealm.RealmUri
                };

                app.UseWsFederationAuthentication(wsFederation);
            }

            app.SetDefaultSignInAsAuthenticationType(WsFederationAuthenticationDefaults.AuthenticationType);
        }
    }
}