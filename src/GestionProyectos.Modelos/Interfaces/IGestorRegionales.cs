using GestionProyectos.Modelos.Peticiones.Regionales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IGestorRegionales
    {
        CrearRegionalResponse Crear(CrearRegional peticion);
        ActualizarRegionalResponse Actualizar(ActualizarRegional peticion);
        BorrarRegionalReponse Borrar(BorrarRegional peticion);
        ConsultarRegionalesResponse Consultar(ConsultarRegionales peticion);

    }

   
}
