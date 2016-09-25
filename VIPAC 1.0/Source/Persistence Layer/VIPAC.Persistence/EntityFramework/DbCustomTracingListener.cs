using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clutch.Diagnostics.EntityFramework;
using VIPAC.Domain;
using VIPAC.Repository;
using StructureMap;

namespace VIPAC.Persistence.EntityFramework
{
    /// <summary>
    /// Implementation of IDbTracingListener
    /// Class is used for tracing all SQL Queries to the entity framework database
    /// </summary>
    public class DbCustomTracingListener : IDbTracingListener
    {      
        public void CommandExecuting(DbTracingContext context)
        {
            var cadena = context.Command.CommandText;

            if (!cadena.ToUpper().StartsWith("SELECT"))
            {
                //var repository = ObjectFactory.GetInstance<ILogGeneralRepository>();

                //LogGeneral log = new LogGeneral { Fecha = DateTime.Now, UsuarioId = 1, Descripcion = cadena };
                //repository.Add(log);

                var cadenaQuery = cadena;

                foreach (DbParameter parametro in context.Command.Parameters)
                {
                    cadenaQuery = cadenaQuery.Replace(parametro.ParameterName, string.Format("'{0}'", parametro.Value.ToString()));
                }

                if (MessageDispatcher.QueriesQueue != null)
                    MessageDispatcher.QueriesQueue.Add(cadenaQuery);
            }
        }

        public void CommandFinished(DbTracingContext context)
        {
        }

        public void ReaderFinished(DbTracingContext context)
        {            
           
        }

        public void CommandFailed(DbTracingContext context)
        {
        }

        public void CommandExecuted(DbTracingContext context)
        {
        }
    }
}
