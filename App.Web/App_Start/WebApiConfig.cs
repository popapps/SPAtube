using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace App.Web
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional, action = "Get" }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
    }
}
