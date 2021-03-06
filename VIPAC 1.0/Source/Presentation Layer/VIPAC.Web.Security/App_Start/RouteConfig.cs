﻿using System.Web.Mvc;
using System.Web.Routing;

// ReSharper disable once CheckNamespace

namespace VIPAC.Web.Security
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}"
            );
        }
    }
}