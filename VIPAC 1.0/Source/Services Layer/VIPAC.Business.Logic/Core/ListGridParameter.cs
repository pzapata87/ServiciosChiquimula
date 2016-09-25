using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VIPAC.Business.Logic.Interfaces;
using VIPAC.Common;
using VIPAC.Common.Enum;
using VIPAC.Domain.Core;

namespace VIPAC.Business.Logic.Core
{
    public class ListGridParameter<T, TResult>
        where T : EntityBase
        where TResult : class
    {
        public ListGridParameter()
        {
            MostrarSoloActivos = true;
        }

        private IGridPaging<T> _businessLogicClass;
        public IGridPaging<T> BusinessLogicClass
        {
            get
            {
                return _businessLogicClass;
            }
            set
            {
                _businessLogicClass = value;
                CountMethod = _businessLogicClass.Count;
                ListMethod = _businessLogicClass.GetAll;
            }
        }

        private Expression<Func<T, bool>> _filtrosAdicionales;
        public Expression<Func<T, bool>> FiltrosAdicionales
        {
            get
            {
                return _filtrosAdicionales ?? (p => p.Estado == (int)TipoEstado.Activo);
            }
            set
            {
                _filtrosAdicionales = MostrarSoloActivos ? value.And(p => p.Estado == (int)TipoEstado.Activo) : value;
            }
        }

        public FilterGeneric Filter { get; set; }
        public Func<T, TResult> SelecctionFormat { get; set; }

        public Func<Expression<Func<T, bool>>, int> CountMethod { get; set; }
        public Func<GridParameters<T>, IQueryable<T>> ListMethod { get; set; }
        public List<Expression<Func<T, object>>> ObjectIncludeList { get; set; }
        public bool MostrarSoloActivos { get; set; }
    }
}
