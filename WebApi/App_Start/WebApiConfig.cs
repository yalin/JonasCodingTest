using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApi.Logging;


namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
            // Replacing exception logger with the one I created
            // It catches all exceptions in web api and then handles (logs) in it
            config.Services.Replace(typeof(IExceptionLogger), new ApiExceptionLogger());

            //Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
