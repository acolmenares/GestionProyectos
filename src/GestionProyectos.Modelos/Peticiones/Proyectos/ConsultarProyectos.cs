using GestionProyectos.Modelos.Entidades;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Proyectos
{
    public class ConsultarProyectos:IReturn<ConsultarProyectosResponse>
    {
    }

    public class ConsultarProyectosResponse:ResponseBase<List<Proyecto>>,IHasResponseStatus
    {
        public ConsultarProyectosResponse()
        {
            Dato = new List<Proyecto>();
        }
    }


	public class EncontrarProyecto : QueryDb<Proyecto>
	{
	}
}
