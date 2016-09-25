using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common;
using VIPAC.Web.Core;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace VIPAC.Web.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Headers.Contains("Authorization-Token"))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.GetValues("Authorization-Token").FirstOrDefault();
                string decodedToken = Encriptador.Desencriptar(authToken);

                string[] partes = decodedToken.Split(':');

                string username = partes[0];
                string password = partes[1];
                string vigencia = partes[2];

                if (long.Parse(DateTime.Now.ToString("yyyyMMddHHmm")) >= long.Parse(vigencia) &&
                    long.Parse(DateTime.Now.ToString("yyyyMMddHHmm")) <= long.Parse(vigencia) + 2)
                {
                    var usuarioBL = DependencyResolver.Current.GetService<IUsuarioBL>();
                    var usuario = usuarioBL.Login(username, password);

                    if (usuario != null)
                    {
                        HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(usuario), new string[] {});
                        base.OnActionExecuting(actionContext);
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.GatewayTimeout);
                }
            }
        }
    }
}