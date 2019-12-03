using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace WebForms_Owin_TestApp.Models
{
    public class ExternalLoginViewModel
    {
        public IEnumerable<AuthenticationDescription> AuthenticationTypes
        {
            get;
            set;
        }

        public string ReturnUrl
        {
            get;
            set;
        }

        public string SelectedAuthenticationType
        {
            get;
            set;
        }
    }
}