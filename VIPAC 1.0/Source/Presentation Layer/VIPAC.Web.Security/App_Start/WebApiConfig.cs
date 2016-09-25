using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Web.Http;
using VIPAC.Web.Security.Core;

// ReSharper disable once CheckNamespace

namespace VIPAC.Web.Security
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional }
               );

            JsonMediaTypeFormatter json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.EnableSystemDiagnosticsTracing();
            config.ParameterBindingRules.Insert(0, SimplePostVariableParameterBinding.HookupParameterBinding);
        }
    }
}
