using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolBankProject.Gateway.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Proxy", id = UrlParameter.Optional }
            );
        }
    }
}