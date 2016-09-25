using System.Web.Http;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common;
using VIPAC.DTO;
using VIPAC.Resources;
using VIPAC.Web.Core;
using VIPAC.Web.Core.Aspects;

namespace VIPAC.Web.Controllers
{
    public class UsuarioController : AuthorizedApiController
    {
        #region Variables Privadas

        private readonly IUsuarioBL _usuarioBL;

        #endregion

        #region Constructor

        public UsuarioController(IUsuarioBL usuarioBL)
        {
            _usuarioBL = usuarioBL;
        }

        #endregion

        #region Métodos Públicos
        
        [HttpPost]
        [Controller]
        public JsonResponse CambiarContrasenia(ChangePasswordDto changePassword)
        {
            var jsonResponse = new JsonResponse {Success = false};
            var user = _usuarioBL.GetById(changePassword.Id);

            if (user != null)
            {
                string passwordAnterior = Encriptador.Desencriptar(user.Password);

                if (passwordAnterior == changePassword.PasswordOld)
                {
                    string passwordNuevo = Encriptador.Encriptar(changePassword.PasswordNew);
                    user.Password = passwordNuevo;
                    _usuarioBL.Update(user);

                    jsonResponse.Success = true;
                    //jsonResponse.Message = Resources.Master.MensajeExito;
                }
                else
                {
                    jsonResponse.Message = Usuario.ClaveNoValida;
                    //jsonResponse.ErrorCode = (int)CodeError.ClaveNoValida;
                }
            }
            else
            {
                jsonResponse.Message = Usuario.UsuarioNoExiste;
                //jsonResponse.ErrorCode = (int)CodeError.UsuarioNoExiste;
            }

            return jsonResponse;
        }

        #endregion
    }
}