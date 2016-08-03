using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Regionales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Servicios
{
    public class ServicioRegionales:ServicioBase
    {
        public IGestorRegionales GestorRegionales { get; set; }

        public CrearRegionalResponse Post(CrearRegional peticion)
        {
            return GestorRegionales.Crear(peticion);
        }

        public ActualizarRegionalResponse Post(ActualizarRegional peticion)
        {
            return GestorRegionales.Actualizar(peticion);
        }

        public BorrarRegionalReponse Post(BorrarRegional peticion)
        {
            return GestorRegionales.Borrar(peticion);
        }

        public ConsultarRegionalesResponse Get(ConsultarRegionales peticion)
        {
            return GestorRegionales.Consultar(peticion);
        }
    }

   
}
