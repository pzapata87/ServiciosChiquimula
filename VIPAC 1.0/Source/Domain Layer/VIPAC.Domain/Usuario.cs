using VIPAC.Domain.Core;

namespace VIPAC.Domain
{
    public class Usuario : EntityExtension<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
