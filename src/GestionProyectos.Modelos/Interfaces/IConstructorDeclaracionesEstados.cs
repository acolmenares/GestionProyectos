using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IConstructorDeclaracionesEstados
    {
        List<DeclaracionesEstados> Data { get; }
    }
}
