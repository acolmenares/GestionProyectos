using GestionProyectos.Modelos.Peticiones.Objetivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IFabricaReglas
    {
        // IReglasObjetivoDosUno ReglasObjetivoDosUno(DatosObjetivoDosUno datosObjetivo,ITransformoFechas transformoFechas);
        IReglasObjetivoDosUno ReglasObjetivoDosUno();
    }
}
