using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Peticiones.Regionales;
using GestionProyectos.Modelos.Entidades;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorRegionales : GestorBase, IGestorRegionales
    {
        public  ActualizarRegionalResponse Actualizar(ActualizarRegional peticion)
        {
            // utilizar el metodo Actualizar para hacer una peticion al repositorio
            // en este caso Actualizar el registro de la regional
            // si necesita mas operaciones con el repositorio
            // ejecutar Fabrica.Ejecutar
            var dato = Actualizar<Regional>(peticion);
            return new ActualizarRegionalResponse { Dato = dato };
        }

        public BorrarRegionalReponse Borrar(BorrarRegional peticion)
        {
            Borrar<Regional,BorrarRegional>(peticion);
            return new BorrarRegionalReponse();
        }

        public ConsultarRegionalesResponse Consultar(ConsultarRegionales peticion)
        {
            var r = Consultar<Regional>(peticion);
            return new ConsultarRegionalesResponse { Dato = r };
        }

        public CrearRegionalResponse Crear(CrearRegional peticion)
        {
            var dato = Actualizar<Regional>(peticion);
            return new CrearRegionalResponse { Dato = dato };
        }
    }
}
