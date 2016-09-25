using System;
using System.Linq.Expressions;
using VIPAC.Common.Enum;

namespace VIPAC.Common
{
    public class GridParameters<T> where T : class
    {
        public string ColumnOrder { get; set; }
        public TipoOrden OrderType { get; set; }
        public int Start { get; set; }
        public int CurrentPage { get; set; }
        public int AmountRows { get; set; }
        public Expression<Func<T, bool>> WhereFilter { get; set; }
    }
}
