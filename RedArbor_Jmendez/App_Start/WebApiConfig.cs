using RedArbor_Jmendez.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

namespace RedArbor_Jmendez
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new NinjectR();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
