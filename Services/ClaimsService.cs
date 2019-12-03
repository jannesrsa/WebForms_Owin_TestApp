using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WsFederation;
using SourceCode.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using WebForms_Owin_TestApp.Helpers;

namespace WebForms_Owin_TestApp.Services
{
    public class ClaimsService
    {
        public ClaimsService()
        {
            var claimsServer = ConnectionHelper.GetServer<ClaimsManagement>();
            using (claimsServer.Connection)
            {
                CurrentRealm = claimsServer.GetRealm("https://k2.denallix.com/WebForms_Owin_TestApp/");

                RealmPathBase = new Uri(CurrentRealm.RealmUri).AbsolutePath;

                Issuers = CurrentRealm.Issuers
                    .Where(iss => !string.IsNullOrWhiteSpace(iss.MetadataUrl) && iss.UseForLogin);
            }
        }

        public Realm CurrentRealm { get; }

        public IEnumerable<Issuer> Issuers { get; }

        public string RealmPathBase { get; }

        public IEnumerable<AuthenticationDescription> AuthenticationTypes
        {
            get
            {
                return this.Issuers.Select(i => new AuthenticationDescription()
                {
                    Caption = i.Name,
                    AuthenticationType = i.IssuerName
                });
            }
        }

        public string AuthenticationTypeForIssuer(Issuer issuer)
        {
            return $"{WsFederationAuthenticationDefaults.AuthenticationType}-{issuer.IssuerName}";
        }

        public string CallbackPathForIssuer(Issuer issuer)
        {
            return $"{this.RealmPathBase.ToLower()}signin-wsfed{issuer.ID.ToString()}";
        }
    }
}