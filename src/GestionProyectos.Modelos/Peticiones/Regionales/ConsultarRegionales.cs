using GestionProyectos.Modelos.Entidades;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Regionales
{
    public class ConsultarRegionales : IReturn<ConsultarRegionalesResponse>
    {
    }

    public class ConsultarRegionalesResponse : ResponseBase<List<Regional>>, IHasResponseStatus
    {
        public ConsultarRegionalesResponse()
        {
            Dato = new List<Regional>();
        }
    }
}
