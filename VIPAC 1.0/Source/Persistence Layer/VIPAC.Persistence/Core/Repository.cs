using VIPAC.Repository.RepositoryContracts;

namespace VIPAC.Persistence.Core
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : class
    {
    }
}