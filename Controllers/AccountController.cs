using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using WebForms_Owin_TestApp.Models;
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("account/externallogin")]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        [Route("account/externallogincallback")]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            return RedirectToLocal(returnUrl);
        }

        [AllowAnonymous]
        [Route("account/login")]
        public ActionResult Login(string returnUrl)
        {
            var claimsService = new ClaimsService();
            var externalLoginViewModel = new ExternalLoginViewModel()
            {
                ReturnUrl = returnUrl,
                AuthenticationTypes = claimsService.AuthenticationTypes
            };

            return View(externalLoginViewModel);
        }

        [Route("account/logout")]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToLocal("~/");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}