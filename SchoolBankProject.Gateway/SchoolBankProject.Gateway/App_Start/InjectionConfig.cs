using SchoolBankProject.Gateway.Services.AccountService;
using SchoolBankProject.Gateway.Services.CustomerService;
using SchoolBankProject.Gateway.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace SchoolBankProject.Gateway.App_Start
{
    public static class InjectionConfig
    {
        public static void RegisterSimpleInjection(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //Register dependency injection
            container.Register<IAccountGateway, AccoutGatewayServices>(Lifestyle.Scoped);
            container.Register<ICustomerGateway, CustomerGatewayServices>(Lifestyle.Scoped);
            
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}