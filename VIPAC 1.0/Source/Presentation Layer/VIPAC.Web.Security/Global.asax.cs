using System.Reflection;
using log4net;
using log4net.Config;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VIPAC.Domain;
using VIPAC.IoC.App_Start;
using VIPAC.Persistence;
using VIPAC.Persistence.EntityFramework;
using VIPAC.Web.Security.Handlers;

namespace VIPAC.Web.Security
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            Log.Info("Se inicio el site... Global.asax");
            AreaRegistration.RegisterAllAreas();

            Database.SetInitializer(new ContextInitializer());
            Database.SetInitializer<DbContextBase>(null);

            PersistenceConfigurator.Configure("DefaultConnection", typeof(Usuario).Assembly, typeof(ConnectionFactory).Assembly);
            StructuremapMvc.Start();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());
            Log.Info("Se agregó el handler para Cors");
        }
    }
}