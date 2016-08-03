using GestionProyectos.Modelos.Entidades;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Proyectos
{
    public class ActualizarProyecto:IReturn<ActualizarProyectoResponse>
    {
		public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }

    public class ActualizarProyectoResponse : ResponseBase<Proyecto>, IHasResponseStatus
    {
        
    }

}
