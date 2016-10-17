﻿using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Multi_Language.DataApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi2",
            //    routeTemplate: "api/{idProject}",
            //    defaults: new {
            //        controller = "Phrases",
            //        action="Get",
            //        idProject = RouteParameter.Optional
            //    }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi3",
            //    routeTemplate: "api/{idProject}/{initials}/{idPhrase}",
            //    defaults: new
            //    {
            //        controller = "Phrases"
            //    }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}