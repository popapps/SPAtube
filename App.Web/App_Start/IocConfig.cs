using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using App.Data;
using App.Data.Contracts;
using App.Data.Helpers;
using Ninject;

namespace App.Web
{
    public class IocConfig
    {
        internal static void RegisterIoc(HttpConfiguration configuration)
        {
            var kernel = new StandardKernel();
            Bind(kernel);

            // dico a WebApi come usare Ninject
            configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            //var uow = kernel.Get<IAppUow>();
            //uow.Migrate();

        }

        internal static void Bind(IKernel kernel)
        {
            kernel.Bind<IAppUow>().To<AppUow>();
            kernel.Bind<RepositoryFactories>().To<RepositoryFactories>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();
        }
    }
}