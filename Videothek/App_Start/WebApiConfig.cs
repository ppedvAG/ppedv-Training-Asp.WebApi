using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Videothek.Formatter;

namespace Videothek
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            config.Formatters.Add(new CsvFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Attributrouting in Bsp ist besser
            //config.Routes.MapHttpRoute(
            //    name: "ByGenre",
            //    routeTemplate: "api/genre/{id}/movie",
            //    defaults: new {controller = "Movie", action= "GetByGenre" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
