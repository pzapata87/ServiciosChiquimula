using System;

namespace VIPAC.Domain.Core
{
    public class EntityExtension<TId> : Entity<TId>
    {
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class EntityExtension : EntityBase
    {
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}