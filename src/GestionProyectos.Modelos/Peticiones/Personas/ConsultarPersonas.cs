using System;
using System.Collections.Generic;
using GestionProyectos.Modelos.Entidades;
using ServiceStack;

namespace GestionProyectos.Modelos.Peticiones.Personas
{
	public class ConsultarPersonas : IReturn<ConsultarPersonasResponse>
	{
		public ConsultarPersonas()
		{
		}
	}

	public class ConsultarPersonasResponse : ResponseBase<List<Persona>>, IHasResponseStatus
	{
		public ConsultarPersonasResponse()
		{
			Dato = new List<Persona>();
		}
	}


	public class EncontrarPersona : QueryDb<Persona>
	{

	}

}