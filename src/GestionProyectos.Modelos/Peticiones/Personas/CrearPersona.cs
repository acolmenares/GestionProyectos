using System;
using GestionProyectos.Modelos.Entidades;
using ServiceStack;

namespace GestionProyectos.Modelos.Peticiones.Personas
{
	public class CrearPersona:IReturn<CrearPersonaResponse>
	{
		public CrearPersona()
		{
		}

		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}

	public class CrearPersonaResponse: ResponseBase<Persona>, IHasResponseStatus
	{
		
	}
}

