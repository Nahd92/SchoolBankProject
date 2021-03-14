using SchoolBankProject.CustomerAPI.App_Start;
using System.Web.Http;

namespace SchoolBankProject.CustomerAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SimpleInject.RegisterSimpleInjection);
        }
    }
}
