using SchoolBankProjet.API.App_Start;
using System.Web.Http;

namespace SchoolBankProjet.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SimpleInjectionConfig.RegisterSimpleInjection);
        }
    }
}
