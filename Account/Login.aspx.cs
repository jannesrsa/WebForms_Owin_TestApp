using System;
using System.Web;
using System.Web.UI;
using WebForms_Owin_TestApp.Services;

namespace WebForms_Owin_TestApp.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // usually this will be injected via DI. but creating this manually now for brevity
                var authenticationManager = Context.GetOwinContext().Authentication;
                var authService = new AdAuthenticationService(authenticationManager);

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
        }
    }
}