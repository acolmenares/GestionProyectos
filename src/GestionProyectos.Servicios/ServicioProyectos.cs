using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Proyectos;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionProyectos.Servicios
{
	public class ServicioProyectos:ServicioBase
    {
        public IGestorProyectos GestorProyectos { get; set; }

        public CrearProyectoResponse Post(CrearProyecto peticion)
        {
			return GestorProyectos.Crear(peticion);
        }

        public ActualizarProyectoResponse Post (ActualizarProyecto peticion)
        {
            return GestorProyectos.ActualizarProyecto(peticion);
        }

        public BorrarProyectoReponse Post (BorrarProyecto peticion)
        {
            return GestorProyectos.Borrar(peticion);
        }

        public ConsultarProyectosResponse Get(ConsultarProyectos peticion)
        {
            return GestorProyectos.Consultar(peticion);
        }
    }
}
