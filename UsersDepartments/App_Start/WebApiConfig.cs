using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UsersDepartments
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
          name: "UsersRoute",
          routeTemplate: "api/{controller}/{action}"
      );


            config.Routes.MapHttpRoute(
    name: "UsersRoute2",
    routeTemplate: "api/{controller}/{action}/{id}"
);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}
