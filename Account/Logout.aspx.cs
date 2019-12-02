using Microsoft.Owin.Security.WsFederation;
using System;
using System.Web;

namespace WebForms_Owin_TestApp.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Signout();
            LoginHelper.RedirectToReturnUrl("~/", this.Response);
        }

        private void Signout()
        {
            LoginHelper.AuthenticationManager.SignOut(WsFederationAuthenticationDefaults.AuthenticationType);
            HttpContext.Current.User = null;
        }
    }
}