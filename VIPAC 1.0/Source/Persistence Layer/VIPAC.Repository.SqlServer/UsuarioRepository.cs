using VIPAC.Domain;
using VIPAC.Persistence.Core;

namespace VIPAC.Repository.SqlServer
{
    public class UsuarioRepository : RepositoryWithTypedId<Usuario, int>, IUsuarioRepository
    {
    }
}