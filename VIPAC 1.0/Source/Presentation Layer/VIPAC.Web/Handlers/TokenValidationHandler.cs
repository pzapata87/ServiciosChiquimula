using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common;
using VIPAC.Web.Core;

namespace VIPAC.Web.Handlers
{
    public class TokenValidationHandler : DelegatingHandler
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token;

            //Log.Info("ingreso al handler de Token");

            if (request.Headers.Contains("Authorization-Token"))
                token = request.Headers.GetValues("Authorization-Token").FirstOrDefault();
            else
            {
                return ManageError(HttpStatusCode.BadRequest, "Missing Authorization-Token");
            }

            //Log.Info("el token fue encontrado");

            try
            {
                string[] valores = Encriptador.Desencriptar(token).Split(':');
                string userName = valores[0];
                string password = valores[1];

                var usuarioBL = DependencyResolver.Current.GetService<IUsuarioBL>();
                var foundUser = usuarioBL.Login(userName, password);
                //if (foundUser == null || !usuarioBL.HasServiceAccess(foundUser.Id, request.RequestUri.Segments.Last()))
                //{
                    return ManageError(HttpStatusCode.Forbidden, "Unauthorized User");
                //}

                HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(foundUser), new string[] { });

                //Log.Info("el usuario fue encontrado");
            }
            catch (Exception)
            {
                return ManageError(HttpStatusCode.InternalServerError, "Error encountered while attempting to process authorization token");
            }
            
            return base.SendAsync(request, cancellationToken);
        }

        private Task<HttpResponseMessage> ManageError(HttpStatusCode status, string content)
        {
            var message = new HttpResponseMessage(status)
            {
                Content = new StringContent(content)
            };

            var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletionSource.SetResult(message);

            return taskCompletionSource.Task;
        }
    }
}