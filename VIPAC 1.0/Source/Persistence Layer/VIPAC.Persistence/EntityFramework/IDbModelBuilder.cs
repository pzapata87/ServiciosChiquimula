using System;

namespace VIPAC.Persistence.EntityFramework
{
    public interface IDbModelBuilder
    {
        void AddConfiguration(Type entityTypeConfiguration);

        void AddEntity(Type entityType);
    }
}