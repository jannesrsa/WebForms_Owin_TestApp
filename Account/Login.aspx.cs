using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
                if (true) // Authenticate through IdP
                {
                    var claims = GetClaims(); //Get the claims from the headers or db or your user store
                    if (null != claims)
                    {
                        SignIn(claims);
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }

                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                }
                else
                {
                    FailureText.Text = "The credentials are not valid!";
                }
            }
        }

        private IEnumerable<Claim> GetClaims()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, Email.Text));
            claims.Add(new Claim(ClaimTypes.Name, Email.Text));
            return claims;
        }

        private void SignIn(IEnumerable<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            //This uses OWIN authentication
            LoginHelper.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            LoginHelper.AuthenticationManager.SignIn(
                new AuthenticationProperties()
                {
                    IsPersistent = false
                }
                , claimsIdentity);
        }
    }
}