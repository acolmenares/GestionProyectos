using GestionProyectos.Modelos.Peticiones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IGestorRango
    {
        QueryResponse<Rango> Consultar(ConsultarRango peticion);
        CrearResponse Crear(ITengoFechaRadicacionDesdeHasta peticion);
        BorrarResponse Borrar(ITengoFechaRadicacionDesdeHasta peticion);
    }
}
