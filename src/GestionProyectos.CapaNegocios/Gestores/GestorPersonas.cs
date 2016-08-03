using System;
using GestionProyectos.Modelos;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones;
using GestionProyectos.Modelos.Peticiones.Personas;
using ServiceStack;
using ServiceStack.Web;

namespace GestionProyectos.CapaNegocios.Gestores
{
	public class GestorPersonas : GestorBase, IGestorPersonas
	{
		public ActualizarPersonaResponse Actualizar(ActualizarPersona peticion)
		{
			var dato = Actualizar<Persona>(peticion);
			return new ActualizarPersonaResponse { Dato = dato };
		}

		public BorrarPersonaReponse Borrar(BorrarPersona peticion)
		{
			Borrar<Persona, BorrarPersona>(peticion);
			return new BorrarPersonaReponse();
		}

		public ConsultarPersonasResponse Consultar(ConsultarPersonas peticion)
		{
			var dato = Consultar<Persona>(peticion);
			return new ConsultarPersonasResponse { Dato = dato };
		}


		public  QueryResponse<Persona> Consultar(IQueryDb<Persona> modelo, IRequest peticion)
		{
			return base.Consultar(modelo, peticion);
        }


		public CrearPersonaResponse Crear(CrearPersona peticion)
		{
			var dato = Crear<Persona>(peticion);
			return new CrearPersonaResponse { Dato = dato };
		}
	}
}

