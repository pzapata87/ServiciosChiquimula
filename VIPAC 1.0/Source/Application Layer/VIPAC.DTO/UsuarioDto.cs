using VIPAC.DTO.Core;

namespace VIPAC.DTO
{
    public class UsuarioDto : EntityDto<int>
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
    }
}