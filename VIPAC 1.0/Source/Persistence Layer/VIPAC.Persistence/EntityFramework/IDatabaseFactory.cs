using System;

namespace VIPAC.Persistence.EntityFramework
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContextBase Get();
    }
}