using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Peticiones.Objetivos;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class FabricaReglas : IFabricaReglas
    {
        /*public IReglasObjetivoDosUno ReglasObjetivoDosUno(DatosObjetivoDosUno datosObjetivo, ITransformoFechas transformoFechas)
        {
            return new ReglasObjetivoDosUno() { DatosObjetivo = datosObjetivo, TransformoFechas=transformoFechas };
        }
        */

        public IReglasObjetivoDosUno ReglasObjetivoDosUno()
        {
            return new ReglasObjetivoDosUno();
        }
    }
}
