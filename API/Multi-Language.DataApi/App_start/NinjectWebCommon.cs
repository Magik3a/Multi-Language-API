//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Multi_Language.DataApi.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Multi_Language.DataApi.App_Start.NinjectWebCommon), "Stop")]

using System.Web.Configuration;
using Multi_language.Services;

namespace Multi_Language.DataApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject.Extensions.Conventions;
    using Ninject;
    using Multi_language.Data;
    using System.Web.Http;
    using System.Reflection;

    public static class NinjectWebCommon
    {
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                RegisterServices(kernel);
                // GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }


        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel
    .Bind<IDbContext>()
    .To<MultiLanguageDbContext>();

            kernel.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));


            kernel.Bind(b => b.FromAssemblyContaining<Multi_language.Services.LanguagesService>()
            .SelectAllClasses()
            .BindDefaultInterface());
        }
    }
}
