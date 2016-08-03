using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Web;

namespace GestionProyectos.CAD
{
    public class QueryResponseFactory : IQueryResponseFactory
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public IAutoQueryData AutoQueryData { get; set; }

        public QueryResponse<T> Consultar<T>(IQueryDb<T> modelo, IRequest peticion)
        {
            var q = AutoQuery.CreateQuery<T>(modelo, peticion);
            return AutoQuery.Execute(modelo, q);
        }

        public QueryResponse<T> Consultar<T>(IQueryData<T> modelo, IRequest peticion, IEnumerable<T> data, bool ignoreMaxLimit = false)
        {
            var source = new MemoryDataSource<T>(new QueryDataContext { Dto = modelo, Request = peticion, DynamicParams = peticion.GetRequestParams() }, data);
            var q = AutoQueryData.CreateQuery<T>(modelo, peticion, source);
            if (ignoreMaxLimit)
            {
                q.Rows = null;
                q.Offset = null;
            }
            return AutoQueryData.Execute(modelo, q);
        }
    }
}
