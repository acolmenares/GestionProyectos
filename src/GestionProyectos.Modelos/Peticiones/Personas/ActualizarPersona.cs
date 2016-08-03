using System;
using GestionProyectos.Modelos.Entidades;
using ServiceStack;

namespace GestionProyectos.Modelos.Peticiones.Personas
{
	public class ActualizarPersona:IReturn<ActualizarPersonaResponse>
	{
		public ActualizarPersona()
		{
		}
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}

	public class ActualizarPersonaResponse: ResponseBase<Persona>, IHasResponseStatus
	{ 
	}
}

