using SchoolBankProject.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.RepositoryWrapper;
using SchoolBankProject.Services.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;

namespace SchoolBankProjet.API.App_Start
{
    public static class SimpleInjectionConfig
    {
        public static void RegisterSimpleInjection(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            //Register dependency injection

            container.Register<IAccountServices, AccountServices>(Lifestyle.Scoped);
            container.Register<ICustomerServices, CustomerServices>(Lifestyle.Scoped);
            container.Register<ITransactionServices, TransactionsService>(Lifestyle.Scoped);

            container.Register<SchoolBankContext>(Lifestyle.Scoped);
            container.Register<IRepositoryWrapper, RepositoryWrapper>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}