using GestionProyectos.Modelos.Peticiones.Proyectos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IGestorProyectos
    {
        ConsultarProyectosResponse Consultar(ConsultarProyectos peticion);
        CrearProyectoResponse Crear(CrearProyecto peticion);
        ActualizarProyectoResponse ActualizarProyecto(ActualizarProyecto peticion);
        BorrarProyectoReponse Borrar(BorrarProyecto peticion);
    }
}
