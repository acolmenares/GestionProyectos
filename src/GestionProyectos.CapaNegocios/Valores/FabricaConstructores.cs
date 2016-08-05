using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System.Collections.Generic;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class FabricaConstructores : IFabricaContructores
    {


        public IConstructorObjetivoDosUno ConstructorObjetivoDosUno(DatosObjetivoDosUno datos, IReglasObjetivoDosUno reglas, ITransformoFechas transformoFechas)
        {
            return new ConstructorObjetivoDosUno() { DatosObjetivo = datos, Reglas = reglas, TransformoFechas = transformoFechas };
        }

        public IConstructorDeclaracionesEstados ContructorDeclaracionesEstados(ILogicaValoresEstados logicaValoresEstados,TablasRango tablas)
        {
            return new ConstructorDeclaracionesEstados
            {
                LogicaValoresEstados = logicaValoresEstados,
                Tablas = tablas,
            };
        }
    }
}
