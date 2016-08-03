using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IFabricaContructores
    {
        IConstructorDeclaracionesEstados ContructorDeclaracionesEstados(ILogicaValoresEstados logicaValoresEstados,
            List<Declaracion> declaraciones, List<Personas> personas, List<SubTablas> subtablas,
            List<PersonasContactos> personasContactos,
            List<DeclaracionEstados> declaracionEstados,
            List<Programacion> programacion);

        IConstructorObjetivoDosUno ConstructorObjetivoDosUno(DatosObjetivoDosUno datosObjetivo,
            IReglasObjetivoDosUno reglas, ITransformoFechas transformoFechas);

    }

    
}
 

 

