using log4net;
using System;
using System.Web.Http;

namespace VIPAC.Web.Security.Core
{
    public class BaseController : ApiController
    {
        #region Variables Privadas

        protected static readonly ILog Logger = LogManager.GetLogger(string.Empty);

        #endregion

        #region Métodos

        #region Control Error

        protected void LogError(Exception exception)
        {
            Logger.Error(string.Format("Mensaje: {0} Trace: {1}", exception.Message, exception.StackTrace));
        }

        #endregion

        #endregion
    }
}
