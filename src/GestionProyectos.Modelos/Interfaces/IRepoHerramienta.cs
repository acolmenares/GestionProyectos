using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IRepoHerramienta
    {
        T Consultar<T>(string rango, Func<T> tFunc) ;

        List<T> Consultar<T>(string rango, Func<List<T>> tListFunc);

        void Crear<T>(string rango, Func<T> dataFunc);

        //List<Declaracion> ConsultarDeclaraciones<T>(QueryDataDeclaracion<T> modelo, Func<List<Declaracion>> p, List<SubTablas> subtablas, bool crearArchivoDeclaraciones=false);

        void CrearRango(ITengoFechaRadicacionDesdeHasta rango, Func<TablasRango> tablasFunc);

        List<Rango> ConsultarRango();

        List<Rango> ConsultarRango(ConsultarRango peticion);

        void BorrarRango(string rango);

        //QueryResponse<DeclaracionesEstados> QueryDeclaracionesEstados(string rango, Func<QueryResponse<DeclaracionesEstados>> dataFunc);

        //TablasRango ConsultarTablas(string rango);

    }
}
