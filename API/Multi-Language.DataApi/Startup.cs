﻿using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Multi_Language.DataApi.App_Start;
using Multi_Language.DataApi.Providers;
using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Hangfire;
using Microsoft.AspNet.SignalR;
using Multi_Language.DataApi.Tasks;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof(Multi_Language.DataApi.Startup))]
namespace Multi_Language.DataApi
{

    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {

            AutoMapperConfig.RegisterMappings(Assembly.Load(Assembly.GetExecutingAssembly().FullName));

            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var ninjectKernel = NinjectWebCommon.CreateKernel();
            config.DependencyResolver = new NinjectResolver(ninjectKernel);

            SwaggerConfig.Register(config);

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
               .UseSqlServerStorage("DefaultConnection");

            GlobalConfiguration.Configuration.UseNinjectActivator(kernel);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            var processorAndRamUsageTask = kernel.Get<IProcessorAndRamUsageTask>();
            RecurringJob.AddOrUpdate(() => processorAndRamUsageTask.CallWebApi(), Cron.Minutely);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new AuthorizationServerProvider(),
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
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);

        }
    }

}