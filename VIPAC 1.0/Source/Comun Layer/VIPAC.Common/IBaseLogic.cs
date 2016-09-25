using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VIPAC.Common
{
    public interface IBaseLogic<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        int Count(string @where = "");
        IList<T> GetAll(string sidx, string sord, int rows, int page, string @where);
    }
}