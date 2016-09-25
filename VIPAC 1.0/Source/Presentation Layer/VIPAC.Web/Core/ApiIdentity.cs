using System;
using System.Security.Principal;
using VIPAC.Domain;

namespace VIPAC.Web.Core
{
    public class ApiIdentity : IIdentity
    {
        public ApiIdentity(Usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            User = user;
        }

        public Usuario User { get; private set; }

        public string Name
        {
            get { return User.UserName; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}