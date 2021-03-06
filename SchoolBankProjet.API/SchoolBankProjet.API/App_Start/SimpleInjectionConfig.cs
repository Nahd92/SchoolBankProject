using SchoolBankProject.Data;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Repositories;
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


            container.Register<IBankAccountRepository, BankAccountRepository>(Lifestyle.Scoped);
            container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
            container.Register<ITransactionRepository, TransactionsRepository>(Lifestyle.Scoped);
            container.Register<IIdentityRepository, IdentityRepository>(Lifestyle.Scoped);

            //  container.Register<SchoolBankContext>(Lifestyle.Scoped);
            //container.Register<LinqDataDataContext>(Lifestyle.Scoped);
            container.Register<IRepositoryWrapper, RepositoryWrapper>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}