using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using log4net.Config;
using VIPAC.Domain;
using VIPAC.IoC.App_Start;
using VIPAC.Persistence;
using VIPAC.Persistence.EntityFramework;
using VIPAC.Web.Handlers;

namespace VIPAC.Web
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Database.SetInitializer(new ContextInitializer());
            Database.SetInitializer<DbContextBase>(null);

            PersistenceConfigurator.Configure("DefaultConnection", typeof (Usuario).Assembly,
                typeof (ConnectionFactory).Assembly);
            StructuremapMvc.Start();

            XmlConfigurator.Configure();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpsHandler());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new TokenValidationHandler());
        }
    }
}