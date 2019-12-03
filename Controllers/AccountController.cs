using Microsoft.Owin.Security;
using Microsoft.Owin.Security.WsFederation;
using System.Web;
using System.Web.Mvc;
using WebForms_Owin_TestApp.Services;

namespace WebForms_Owin_TestApp.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager _authenticationManager;

        public AccountController()
        {
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                _authenticationManager = _authenticationManager ?? HttpContext.GetOwinContext().Authentication;
                return _authenticationManager;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            AuthenticationManager.Challenge(
                new AuthenticationProperties()
                {
                    RedirectUri = returnUrl
                },
                WsFederationAuthenticationDefaults.AuthenticationType);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "~/";
            }

            return Redirect(returnUrl);
        }

        public ActionResult Login(string returnUrl)
        {
            var claimsService = new ClaimsService();
            var externalLoginViewModel = new Models.ExternalLoginViewModel()
            {
                ReturnUrl = returnUrl,
                AuthenticationTypes = claimsService.AuthenticationTypes
            };

            return View(externalLoginViewModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Redirect("/");
        }
    }
}