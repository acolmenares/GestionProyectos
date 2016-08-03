using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;
using ServiceStack.Web;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IGestorObjetivoDosUno
    {
        QueryResponse<ObejtivoDosUno> Consultar(QueryObjetivoDosUno query, IRequest request);
    }
}
