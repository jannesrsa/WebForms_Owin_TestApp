using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using System.Web.UI;

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
                if (Membership.ValidateUser(Name.Text, Password.Text))
                {
                    // get user info
                    var user = Membership.GetUser(Name.Text);

                    // build a list of claims
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ProviderUserKey.ToString()));

                    if (Roles.Enabled)
                    {
                        foreach (var role in Roles.GetRolesForUser(user.UserName))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }

                    // create the identity
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

                    Context.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = RememberMe.Checked
                    },
                    identity);

                    LoginHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "The credentials are not valid!";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}