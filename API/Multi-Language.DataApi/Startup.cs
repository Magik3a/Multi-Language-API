using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Multi_Language.DataApi.App_Start;
using Multi_Language.DataApi.Providers;
using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Microsoft.AspNet.SignalR;
using Multi_Language.DataApi.App_start;
using Multi_Language.DataApi.Tasks;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof(Multi_Language.DataApi.Startup))]
namespace Multi_Language.DataApi
{
    public class ApplicationPreload : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start();
        }
    }
    public class Startup
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Start();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            HangfireBootstrapper.Instance.Stop();
        }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {

            AutoMapperConfig.RegisterMappings(Assembly.Load(Assembly.GetExecutingAssembly().FullName));

            HttpConfiguration config = new HttpConfiguration();


            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var ninjectKernel = NinjectWebCommon.CreateKernel();
            config.DependencyResolver = new NinjectResolver(ninjectKernel);

            SwaggerConfig.Register(config);

            ConfigureOAuth(app, ninjectKernel.Get<IBearerTokenExpirationTask>());

            app.UseWebApi(config);

            ConfigureSignalR(app);

            ConfigureBackgroundTasks(app, ninjectKernel);
        }

        public void ConfigureSignalR(IAppBuilder app)
        {

            app.Map("/signalr", map =>
            {
                map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                {
                    Provider = new QueryStringOAuthBearerProvider()
                });

                var hubConfiguration = new HubConfiguration
                {
                    Resolver = GlobalHost.DependencyResolver,
                };
                map.RunSignalR(hubConfiguration);
            });
        }

        public void ConfigureBackgroundTasks(IAppBuilder app, IKernel kernel)
        {

            GlobalConfiguration.Configuration
               .UseSqlServerStorage("DefaultConnection").UseConsole();

            GlobalConfiguration.Configuration.UseNinjectActivator(kernel);
            var options = new DashboardOptions
            {
                Authorization = new[]
                {
                    new LocalRequestsOnlyAuthorizationFilter()
                }
            };

            app.UseHangfireDashboard("/hangfire",options);
            app.UseHangfireServer();

            var processorAndRamUsageTask = kernel.Get<IProcessorAndRamUsageTask>();
            RecurringJob.AddOrUpdate(() => processorAndRamUsageTask.CallWebApi(), Cron.Minutely);

        }

        public void ConfigureOAuth(IAppBuilder app, IBearerTokenExpirationTask bearerTokenExpirationTask)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new AuthorizationServerProvider(bearerTokenExpirationTask),
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["FacebookAppID"],
                AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"],
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);

        }


    }

}