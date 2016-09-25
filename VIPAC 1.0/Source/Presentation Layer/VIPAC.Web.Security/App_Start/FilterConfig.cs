using System.Web.Mvc;

// ReSharper disable once CheckNamespace

namespace VIPAC.Web.Security
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}