using System;
using System.Linq.Expressions;
using VIPAC.Domain;

namespace VIPAC.Business.Logic.Interfaces
{
    public interface IUsuarioBL : IGridPaging<Usuario>
    {
        Usuario Login(string username, string password);
        Usuario Get(Expression<Func<Usuario, bool>> where);
        void Add(Usuario entity);
        void Update(Usuario entity);
        Usuario GetById(int id);
    }
}