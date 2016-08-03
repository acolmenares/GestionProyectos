using System;
using ServiceStack;
using ServiceStack.Model;

namespace GestionProyectos.Modelos.Peticiones.Personas
{
	public class BorrarPersona:IReturn<BorrarPersonaReponse>, IHasId<int>
	{
		public BorrarPersona()
		{
		}

		public int Id
		{
			get;
		}

	}

	public class BorrarPersonaReponse:IHasResponseStatus
	{
		public ResponseStatus ResponseStatus { get; set; }
	}
}

