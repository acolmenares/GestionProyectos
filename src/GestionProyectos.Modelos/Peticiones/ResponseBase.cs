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
}
