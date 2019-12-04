using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms_Owin_TestApp.Services;

namespace WebForms_Owin_TestApp.Account
{
    public partial class Login : Page
    {
        private IAuthenticationManager _authenticationManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                _authenticationManager = _authenticationManager ?? Context.GetOwinContext().Authentication;
                return _authenticationManager;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var claimsIssuers = AuthenticationManager.GetExternalAuthenticationTypes();

                // Add items like below example
                foreach (var authenticationType in claimsIssuers)
                {
                    ddlClaimsIssuer.Items.Add(new ListItem(authenticationType.Caption, authenticationType.AuthenticationType));
                }
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            var claimsService = new ClaimsService();
            var issuer = claimsService.Issuers.FirstOrDefault(i => i.Name == ddlClaimsIssuer.SelectedItem.Text);

            if (issuer == null)
            {
                FailureText.Text = "Invalid issuer";
                ErrorMessage.Visible = true;

                return;
            }

            AuthenticationManager.Challenge(new AuthenticationProperties
            {
                RedirectUri = Request.QueryString["ReturnUrl"]
            },
            issuer.IssuerName);
        }
    }
}