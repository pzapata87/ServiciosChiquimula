using System;
using System.Linq;
using System.Linq.Expressions;
using VIPAC.Aspects;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common.Enum;
using VIPAC.Domain;
using VIPAC.Persistence.Aspects;
using VIPAC.Repository;

namespace VIPAC.Business.Logic
{
    [TryCatch(ExceptionTypeExpected = typeof(Exception), RethrowException = true)]
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioRepository _userRepository;

        public UsuarioBL(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public Usuario Login(string username, string password)
        {
            return
                _userRepository.FindOne(
                    p => p.UserName == username && p.Password == password && p.Estado == (int) TipoEstado.Activo);
        }
        
        public Usuario Get(Expression<Func<Usuario, bool>> where)
        {
            return _userRepository.FindOne(where);
        }
        
        [CommitsOperation]
        public void Add(Usuario entity)
        {
            _userRepository.Add(entity);
        }
        
        [CommitsOperation]
        public void Update(Usuario entity)
        {
            _userRepository.Update(entity);
        }
        
        public Usuario GetById(int id)
        {
            return _userRepository.FindOne(p => p.Id == id && p.Estado == (int)TipoEstado.Activo);
        }
        
        public int Count(Expression<Func<Usuario, bool>> where)
        {
            return _userRepository.Count(where);
        }
        
        public IQueryable<Usuario> GetAll(Common.GridParameters<Usuario> parameters)
        {
            return _userRepository.FindAllPaging(parameters);
        }
    }
}