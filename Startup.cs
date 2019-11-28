using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebForms_Owin_TestApp.Startup))]

namespace WebForms_Owin_TestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}