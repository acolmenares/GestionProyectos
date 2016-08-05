using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones
{
    public class ResponseBase<T>: IHasResponseStatus, IHasDato<T>
    {
        public ResponseBase()
        {
            Dato = default(T);
        }

        public T Dato { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }

    public abstract class Consultar<T> : IReturn<QueryResponse<T>>, IReturn
    {

    }

    public abstract class Crear : IReturn<CrearResponse>, IReturn
    {

    }
       

    public abstract class Borrar : IReturn<BorrarResponse>, IReturn
    {

    }

}
