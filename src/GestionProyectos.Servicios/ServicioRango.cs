using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;

namespace GestionProyectos.Servicios
{
    public class ServicioRango:Service
    {
        public IGestorRango Gestor { get; set; }

        public QueryResponse<Rango> Get(ConsultarRango peticion)
        {
            return Gestor.Consultar(peticion);
        }

        public CrearResponse  Get(CrearRango peticion)
        {
            return  Gestor.Crear(peticion);
        }

        public BorrarResponse Get(BorrarRango peticion)
        {
            return Gestor.Borrar(peticion);
        }
    }
}
