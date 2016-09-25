using System;
using System.Web.Http;
using Resources;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common;
using VIPAC.DTO;
using VIPAC.Email.Models;
using VIPAC.Web.Security.Core;
using VIPAC.Web.Security.Core.Aspects;

namespace VIPAC.Web.Security.Controllers
{
    public class LoginController : BaseController
    {
        #region Variables Privadas

        private readonly IUsuarioBL _usuarioBL;

        #endregion

        #region Constructor

        public LoginController(IUsuarioBL usuarioBL)
        {
            _usuarioBL = usuarioBL;
        }

        #endregion

        #region Métodos

        [HttpPost]
        [Controller]
        public JsonResponse Login(LogOnDto logOn)
        {
            var jsonResponse = new JsonResponse {Success = false};

            if (logOn != null && !(String.IsNullOrEmpty(logOn.UserName) || String.IsNullOrEmpty(logOn.Password)))
            {
                string password = Encriptador.Encriptar(logOn.Password);

                var user = _usuarioBL.Login(logOn.UserName, password);
                if (user != null)
                {
                    string textoCodificado =
                        Encriptador.Encriptar(string.Format("{0}:{1}:{2}", logOn.UserName, password,
                            DateTime.Now.ToString("yyyyMMddHHmm")));

                    //userDto.Token = textoCodificado;

                    jsonResponse.Success = true;
                    //jsonResponse.Data = userDto;
                }
                else
                {
                    jsonResponse.Message = Master.LoginFalla;
                }
            }
            else
            {
                jsonResponse.Message = Master.DatosRequeridos;
            }

            return jsonResponse;
        }

        [HttpPost]
        public JsonResponse ResetPassword([FromBody] string userName)
        {
            var jsonResponse = new JsonResponse {Success = false};

            if (!String.IsNullOrEmpty(userName))
            {
                var user = _usuarioBL.Get(p => p.UserName == userName);
                if (user != null && !String.IsNullOrEmpty(user.Email))
                {
                    string guid = Guid.NewGuid().ToString().Split('-')[0];

                    user.Password = Encriptador.Encriptar(guid);
                    _usuarioBL.Update(user);

                    Email.Email.FromDefault()
                        .To(user.Email)
                        .Subject(Master.AsuntoResetClave)
                        .UsingTemplateFromFile(Constantes.PathEmailTemplateResetPassword,
                            new UsuarioModel {Usuario = user.UserName, Password = guid})
                        .Send();

                    jsonResponse.Success = true;
                    jsonResponse.Message = Master.ResetClaveExito;
                }
                else
                {
                    jsonResponse.Message = Master.UsuarioNoExiste;
                }
            }
            else
            {
                jsonResponse.Message = Master.DatosRequeridos;
            }

            return jsonResponse;
        }

        #endregion
    }
}