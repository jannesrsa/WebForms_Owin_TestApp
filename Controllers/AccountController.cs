using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet]
        [Route("account/login")]
        [AllowAnonymous]
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
    }
}