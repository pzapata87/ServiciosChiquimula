using VIPAC.DTO.Core;

namespace VIPAC.DTO
{
    public class ChangePasswordDto : EntityDto<int>
    {
        public string PasswordNew { get; set; }
        public string PasswordOld { get; set; }
    }
}
