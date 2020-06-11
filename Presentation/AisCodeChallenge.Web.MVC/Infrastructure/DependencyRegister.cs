using AisCodeChallenge.Common.Cache;
using AisCodeChallenge.Services.File;
using AisCodeChallenge.Web.MVC.Factories.File;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace AisCodeChallenge.Web.MVC.Infrastructure
{
    public class DependencyRegister
    {
        /// <summary>
        /// Register Services and Factory used as Interface in Application
        /// </summary>
        /// <returns></returns>
        internal static IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterTypes(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region Services
            builder.RegisterType<FileServices>().As<IFileServices>().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheManager>().As<IMemoryCache>().Named<IMemoryCache>("Ais_cache_static").SingleInstance();
            #endregion

            #region Factory
            builder.RegisterType<FileModelFactory>().As<IFileModelFactory>().InstancePerLifetimeScope();
            #endregion

        }

    }
}