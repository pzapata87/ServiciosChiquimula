using System.Web;
using VIPAC.Domain;

namespace VIPAC.Web.Core
{
    public class AuthorizedApiController : BaseController
    {
        #region Constructor

        public AuthorizedApiController()
        {
        }

        #endregion

        #region Propiedades

        public Usuario AuthorizedUser
        {
            get { return ((ApiIdentity) HttpContext.Current.User.Identity).User; }
        }

        #endregion
    }
}