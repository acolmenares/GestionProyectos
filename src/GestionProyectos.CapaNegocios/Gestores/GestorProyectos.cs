using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Peticiones.Proyectos;
using GestionProyectos.Modelos.Entidades;
using ServiceStack;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorProyectos : GestorBase, IGestorProyectos
    {
        public ActualizarProyectoResponse ActualizarProyecto(ActualizarProyecto peticion)
        {
            var dato = Actualizar<Proyecto>(peticion);
            return new ActualizarProyectoResponse { Dato = dato };
        }

        public BorrarProyectoReponse Borrar(BorrarProyecto peticion)
        {
            Borrar<Proyecto, BorrarProyecto>(peticion);
            return new BorrarProyectoReponse();
        }

        public ConsultarProyectosResponse Consultar(ConsultarProyectos peticion)
        {
            var r = Consultar<Proyecto>(peticion);
            return new ConsultarProyectosResponse { Dato = r };
            
        }

        public CrearProyectoResponse Crear(CrearProyecto peticion)
        {
            var dato = Crear<Proyecto>(peticion); 
            return new CrearProyectoResponse { Dato = dato };
        }
    }
}
