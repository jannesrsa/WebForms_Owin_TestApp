using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebForms_Owin_TestApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapMvcAttributeRoutes();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}