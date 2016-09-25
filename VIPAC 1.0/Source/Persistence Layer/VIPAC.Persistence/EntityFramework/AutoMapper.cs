using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using VIPAC.Common;
using VIPAC.Domain.Core;

namespace VIPAC.Persistence.EntityFramework
{
    public class AutoMapper
    {
        private readonly IList<Type> _alreadyMappedEntities = new List<Type>();

        public void Apply(IDbModelBuilder modelBuilder)
        {
            if (PersistenceConfigurator.MappingsAssembly != null)
            {
                MapEntitiesFromMappingConfigurations(modelBuilder);
            }
            if (PersistenceConfigurator.EntititesAssembly != null)
            {
                MapEntitiesByDefaultConventions(modelBuilder);
            }
        }

        private void MapEntitiesByDefaultConventions(IDbModelBuilder modelBuilder)
        {
            var list =
                (from type in
                    PersistenceConfigurator.EntititesAssembly.GetExportedTypes()
                        .Where(p => p.Namespace != "VIPAC.Domain.Core")
                    where
                        UtilsComun.VerificarBaseType(type, typeof(EntityBase))
                    select type).ToList<Type>();

            foreach (var type in list)
            {
                if (!_alreadyMappedEntities.Contains(type))
                {
                    modelBuilder.AddEntity(type);
                }
            }
        }

        private void MapEntitiesFromMappingConfigurations(IDbModelBuilder modelBuilder)
        {
            var list =
                (from type in
                     PersistenceConfigurator.MappingsAssembly.GetTypes()
                         .Where(p => p.Namespace != "VIPAC.Domain.Core")
                 where UtilsComun.VerificarBaseType(type, typeof(EntityTypeConfiguration<>))
                 select type).ToList<Type>();

            foreach (var type in list)
            {
                modelBuilder.AddConfiguration(type);
                var baseType = type.BaseType;
                _alreadyMappedEntities.Add(baseType.GetGenericArguments()[0]);
            }
        }
    }
}