using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IQueryResponseFactory
    {
        QueryResponse<T> Consultar<T>(IQueryDb<T> modelo, IRequest peticion);

        QueryResponse<T> Consultar<T>(IQueryData<T> modelo, IRequest peticion, IEnumerable<T> data, bool ignoreMaxLimit = false);
    }
}
