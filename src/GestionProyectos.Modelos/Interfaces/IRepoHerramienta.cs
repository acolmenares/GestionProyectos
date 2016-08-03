using GestionProyectos.Modelos.Peticiones.Declaraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Entidades;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IRepoHerramienta
    {
        T Consultar<T>(string key, Func<T> tFunc) ;

        void Crear<T>(string key, T data);
        List<Declaracion> ConsultarDeclaraciones<T>(QueryDataDeclaracion<T> modelo, Func<List<Declaracion>> p, List<SubTablas> subtablas, bool crearArchivoDeclaraciones=false);
    }
}
