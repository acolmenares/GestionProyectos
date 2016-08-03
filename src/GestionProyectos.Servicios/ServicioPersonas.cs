using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Personas;
using ServiceStack;

namespace GestionProyectos.Servicios
{
	public class ServicioPersonas:ServicioBase
	{
		public IGestorPersonas Gestor { get; set; }

		public CrearPersonaResponse Post(CrearPersona peticion)
		{
			return Gestor.Crear(peticion);
		}

		public ActualizarPersonaResponse Put(ActualizarPersona peticion)
		{
			return Gestor.Actualizar(peticion);

		}

		public ConsultarPersonasResponse Get(ConsultarPersonas peticion)
		{
			return Gestor.Consultar(peticion);
		}

		public QueryResponse<Persona> Get(EncontrarPersona modelo)
		{
			return Gestor.Consultar(modelo, Request);
		}


		public BorrarPersonaReponse Post(BorrarPersona peticion)
		{
			return Gestor.Borrar(peticion);
		}

	}
}

