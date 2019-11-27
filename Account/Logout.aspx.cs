﻿using Microsoft.Owin.Security.Cookies;
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
            //This uses OWIN authentication
            LoginHelper.AuthenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);

            HttpContext.Current.User = null;
        }
    }
}