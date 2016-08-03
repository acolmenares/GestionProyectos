using ServiceStack;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Regionales
{
    public class BorrarRegional : IReturn<BorrarRegionalReponse>, IHasId<int>
    {
        public int Id { get; set; }
    }

    public class BorrarRegionalReponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }

    }
}
