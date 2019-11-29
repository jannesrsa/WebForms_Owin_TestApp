using Microsoft.Owin.Security;
using System;
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
            if (IsValid)
            {
                if (string.IsNullOrEmpty(ddlClaimsIssuer.SelectedValue))
                {
                    var authService = new AdAuthenticationService(AuthenticationManager);
                    var authenticationResult = authService.SignIn(Name.Text, Password.Text, RememberMe.Checked);

                    if (authenticationResult.IsSuccess)
                    {
                        LoginHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                    else
                    {
                        FailureText.Text = authenticationResult.ErrorMessage;
                        ErrorMessage.Visible = true;
                    }
                }

                FailureText.Text = "External Claims Issuers not configured";
                ErrorMessage.Visible = true;
            }
        }
    }
}