using ServiceStack;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Proyectos
{
    public class BorrarProyecto:IReturn<BorrarProyectoReponse>, IHasId<int>
    {
        public int Id {get;set;}
    }

    public class BorrarProyectoReponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
       
    }
}
