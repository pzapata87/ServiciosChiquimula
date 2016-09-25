using System;
using System.Linq;
using System.Linq.Expressions;
using VIPAC.Common;

namespace VIPAC.Business.Logic.Interfaces
{
    public interface IGridPaging<T> where T : class
    {
        int Count(Expression<Func<T, bool>> @where);
        IQueryable<T> GetAll(GridParameters<T> parameters);
    }
}
