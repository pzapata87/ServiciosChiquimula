using System;
using System.Reflection;
using log4net;
using PostSharp.Aspects;
using VIPAC.Common;
using VIPAC.Common.CustomExceptions;

namespace VIPAC.Web.Security.Core.Aspects
{
    [Serializable]
    public class ControllerAttribute : MethodInterceptionAspect
    {
        #region Variables Privadas

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public ControllerAttribute()
        {
            AspectPriority = 9;
            AttributePriority = 9;
        }

        #endregion

        #region Métodos Públicos

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            try
            {
                args.Proceed();
            }
            catch (InvalidDataException ex)
            {
                args.ReturnValue = GetError(ex);
            }
            catch (Exception ex)
            {
                args.ReturnValue = GetError(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private JsonResponse GetError(Exception ex)
        {
            Log.Error(string.Format("Mensaje: {0} Trace: {1}", ex.Message, ex.StackTrace));

            var jsonResponse = new JsonResponse
            {
                Success = false,
                Message = ex.Message,
                ErrorCode = ex.HResult
            };

            return jsonResponse;
        }

        private JsonResponse GetError(Exception ex, string customMessage, int customErrorCode)
        {
            Log.Error(string.Format("Mensaje: {0} Trace: {1}", ex.Message, ex.StackTrace));

            var jsonResponse = new JsonResponse
            {
                Success = false,
                Message = customMessage,
                ErrorCode = customErrorCode
            };

            return jsonResponse;
        }

        #endregion
    }
}
