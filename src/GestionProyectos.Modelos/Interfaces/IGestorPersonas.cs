using System;
using GestionProyectos.Modelos.Peticiones.Personas;
using ServiceStack;
using ServiceStack.Web;
using GestionProyectos.Modelos.Entidades;

namespace GestionProyectos.Modelos.Interfaces
{
	public interface IGestorPersonas
	{
		CrearPersonaResponse Crear(CrearPersona peticion);
		ActualizarPersonaResponse Actualizar(ActualizarPersona peticion);
		BorrarPersonaReponse Borrar(BorrarPersona peticion);
		ConsultarPersonasResponse Consultar(ConsultarPersonas peticion);
		QueryResponse<Persona> Consultar(IQueryDb<Persona> modelo, IRequest peticion);
	}
}

