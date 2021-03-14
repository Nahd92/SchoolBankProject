using SchoolBankProject.Gateway.App_Start;
using System.Web.Routing;
using System.Web.Http;

namespace SchoolBankProject.Gateway
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            GlobalConfiguration.Configure(InjectionConfig.RegisterSimpleInjection);
        }
    }
}
