using System.Data.Entity.ModelConfiguration;
using VIPAC.Domain;

namespace VIPAC.Persistence.DatabaseMappings
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            Property(p => p.UserName).IsRequired().HasMaxLength(50);
            Property(p => p.Password).HasMaxLength(50);
            Property(p => p.Email).HasMaxLength(50);
        }
    }
}
