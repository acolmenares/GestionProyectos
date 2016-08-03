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

        public IConstructorDeclaracionesEstados ContructorDeclaracionesEstados(ILogicaValoresEstados logicaValoresEstados,
            List<Declaracion> declaraciones,
            List<Personas> personas, 
            List<SubTablas> subtablas, 
            List<PersonasContactos> personasContactos, 
            List<DeclaracionEstados> declaracionEstados, 
            List<Programacion> programacion)
        {
            return new ConstructorDeclaracionesEstados {
                LogicaValoresEstados=logicaValoresEstados,
                Declaraciones= declaraciones,
                Personas= personas,
                SubTablas=subtablas,
                PersonasContactos=personasContactos,
                DeclaracionEstados=declaracionEstados,
                Programacion=programacion
            };
        }
    }
}
