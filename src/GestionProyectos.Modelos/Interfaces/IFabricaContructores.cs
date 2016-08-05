using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IFabricaContructores
    {
        IConstructorDeclaracionesEstados ContructorDeclaracionesEstados(ILogicaValoresEstados logicaValoresEstados,TablasRango tablas  );

        IConstructorObjetivoDosUno ConstructorObjetivoDosUno(DatosObjetivoDosUno datosObjetivo,
            IReglasObjetivoDosUno reglas, ITransformoFechas transformoFechas);

    }

    
}
 

 

