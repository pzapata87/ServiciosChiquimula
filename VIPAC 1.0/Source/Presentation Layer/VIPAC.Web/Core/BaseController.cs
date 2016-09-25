using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Http;
using log4net;
using OpenTravel;
using VIPAC.Business.Logic.Core;
using VIPAC.Common;
using VIPAC.Common.Enum;
using VIPAC.Domain.Core;

namespace VIPAC.Web.Core
{
    public class BaseController : ApiController
    {
        #region Variables Privadas

        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public BaseController()
        {
        }

        #endregion

        #region Métodos

        #region Control Error

        protected void LogError(Exception exception)
        {
            Log.Error(string.Format("Mensaje: {0} Trace: {1}", exception.Message, exception.StackTrace));
        }

        protected ErrorsType GetErrorsType(string msjError)
        {
            return new ErrorsType
            {
                Error = new List<ErrorType> {new ErrorType {Value = msjError}}
            };
        }

        #endregion

        #region Paginación

        protected GenericDouble<PagingResult<TResult>, T> Listar<T, TResult>(Func<Expression<Func<T, bool>>, int> countMethod,
            Func<GridParameters<T>, IQueryable<T>> listMethod, GridParameters<T> parameters, List<Expression<Func<T, object>>> includeList)
            where T : class
            where TResult : class
        {
            var pResult = new PagingResult<TResult>();
            IList<T> list;

            try
            {
                int totalPages = 0;

                var count = countMethod(parameters.WhereFilter);

                if (count > 0 && parameters.AmountRows > 0)
                {
                    if (count % parameters.AmountRows > 0)
                    {
                        totalPages = count / parameters.AmountRows + 1;
                    }
                    else
                    {
                        totalPages = count / parameters.AmountRows;
                    }

                    totalPages = totalPages == 0 ? 1 : totalPages;
                }

                parameters.CurrentPage = parameters.CurrentPage > totalPages ? totalPages : parameters.CurrentPage;

                parameters.Start = parameters.AmountRows * parameters.CurrentPage - parameters.AmountRows;
                if (parameters.Start < 0)
                {
                    parameters.Start = 0;
                }

                pResult.Total = totalPages;
                pResult.Page = parameters.CurrentPage;
                pResult.Records = count;
                pResult.Start = parameters.Start;

                var queryList = listMethod(parameters);

                if (includeList != null)
                {
                    queryList = includeList.Aggregate(queryList, (p, include) => p.Include(include));
                }

                list = queryList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new GenericDouble<PagingResult<TResult>, T>(pResult, list);
        }

        protected JsonResponse ListarGrid<T, TResult>(ListGridParameter<T, TResult> parameter)
            where T : EntityBase
            where TResult : class
        {
            var jsonResponse = new JsonResponse { Success = false };
            try
            {
                var where = UtilsComun.ConvertToLambda<T>(parameter.Filter.WhereRule).And(parameter.FiltrosAdicionales ?? (q => true));

                var parameters = new GridParameters<T>
                {
                    ColumnOrder = parameter.Filter.ColumnOrder,
                    CurrentPage = parameter.Filter.CurrentPage,
                    OrderType = (TipoOrden)Enum.Parse(typeof(TipoOrden), parameter.Filter.OrderType, true),
                    WhereFilter = where,
                    AmountRows = parameter.Filter.AmountRows
                };

                var generic = Listar<T, TResult>(parameter.CountMethod, parameter.ListMethod, parameters, parameter.ObjectIncludeList);
                generic.Value.Rows = generic.List.Select(parameter.SelecctionFormat).ToArray();

                jsonResponse.Success = true;
                jsonResponse.Data = generic.Value;
            }
            catch (Exception ex)
            {
                LogError(ex);
                //jsonResponse.Message = Resources.Master.MensajeFalla;
            }

            return jsonResponse;
        }

        #endregion

        #endregion
    }
}