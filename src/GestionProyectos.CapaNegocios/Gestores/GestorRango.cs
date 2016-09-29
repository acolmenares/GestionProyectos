using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorRango : GestorBase, IGestorRango
    {
        
        public BorrarResponse Borrar(ITengoFechaRadicacionDesdeHasta peticion)
        {
            RepoHerramienta.BorrarRango(TransformoFechas.ConvertirEnRango(peticion));
            return new BorrarResponse();
        }

        public QueryResponse<Rango> Consultar(ConsultarRango peticion)
        {
            var rangos = RepoHerramienta.ConsultarRango(peticion);
            return new QueryResponse<Rango> { Results = rangos, Total = rangos.Count };
        }

        public CrearResponse Crear(ITengoFechaRadicacionDesdeHasta peticion)
        {

            RepoHerramienta.CrearRango(peticion,
                () => Fabrica.Ejecutar(rp =>
                {
                    return CargarTablasRango(rp, peticion, true);
                }));
            return new CrearResponse();
        }
    }
}
