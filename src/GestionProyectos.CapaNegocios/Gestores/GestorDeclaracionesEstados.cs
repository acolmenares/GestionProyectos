using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Web;
using System;
using System.Collections.Generic;

namespace GestionProyectos.CapaNegocios.Gestores
{

    public class GestorDeclaracionesEstados : GestorBase, IGestorDeclaracionesEstados
    {
                
        public ILogicaValoresEstados LogicaValoresEstados { get; set; }
       
        public QueryResponse<DeclaracionesEstados> Consultar(QueryDeclaracionesEstados query, IRequest request)
        {
            var key = GetKey(query);
            return ConsultarCore(query, request, key,crearArchivoDeclaraciones:false);
        }

        public void Crear(CrearDeclaracionesEstados query, IRequest request)
        {
            var queryDE = new QueryDeclaracionesEstados
            {
                IgnoreMaxLimit = true,
                Fecha_RadicacionGreaterThanOrEqualTo = query.Fecha_RadicacionGreaterThanOrEqualTo,
                Fecha_RadicacionLessThanOrEqualTo = query.Fecha_RadicacionLessThanOrEqualTo
            };

            var key = GetKey(queryDE);
            var refrescarCache = !( query.IgnorarCache.HasValue && query.IgnorarCache.Value);

            var data = ConsultarCore(queryDE, request, key, refrescarCache: refrescarCache, crearArchivoDeclaraciones: true);
            RepoHerramienta.Crear(key, data);
        }

        private QueryResponse<DeclaracionesEstados> ConsultarCore(QueryDeclaracionesEstados query, IRequest request, string key, 
            bool refrescarCache=false, bool crearArchivoDeclaraciones=false)
        {
           
            var r = new QueryResponse<DeclaracionesEstados>();

            Fabrica.Ejecutar(rp =>
            {
                var estados = GetFromCache(Cache, key, () =>
                {
                    return RepoHerramienta.Consultar(key, () =>
                    {
                        var tablas = CargarTablas(rp, query, request, Cache,RepoHerramienta, crearArchivoDeclaraciones);
                        var de= FbContructores.ContructorDeclaracionesEstados(LogicaValoresEstados,
                            tablas.Item1,
                            tablas.Item2,
                            tablas.Item3,
                            tablas.Item4,
                            tablas.Item5,
                            tablas.Item6)
                            .Data;
                        return new QueryResponse<DeclaracionesEstados> { Results = de , Total= de.Count};

                    });
                }, refrescarCache);
                r = QueryResponseFactory.Consultar(query, request, estados.Results, ignoreMaxLimit: query.IgnoreMaxLimit.HasValue && query.IgnoreMaxLimit.Value);
            });


            return r;
        }
        

        private static Tuple<List<Declaracion>, List<Personas>, 
            List<SubTablas>, List<PersonasContactos>, 
            List<DeclaracionEstados>, List<Programacion>>  CargarTablas<T>(IRepositorio rp, QueryDataDeclaracion<T> modelo,
            IRequest request, ICacheClient cache, IRepoHerramienta rh, bool crearArchivoDeclaraciones=false)
        {
            var subtablas = GetSubTablas(cache, rp);

            List<Declaracion> declaraciones = rh.ConsultarDeclaraciones(modelo, () =>
            {
                return rp.ConsultarDeclaracion(modelo); // new List<Declaracion>();//
            }, subtablas, crearArchivoDeclaraciones);

            var idsDeclaraciones = declaraciones.ConvertAll(q => q.Id);  
            var personas = rp.ConsultarPersonasDeclaracion(idsDeclaraciones);  //  new List<Personas>();  //
            
            var idsPersonas = personas.ConvertAll(q => q.Id);
            var contactos = rp.ConsultarPersonasContactos(idsPersonas); //, (q) => new { q.Id_Persona, q.Id_Tipo_Contacto, q.Activo, q.Descripcion});
            var dEstados = rp.ConsultarDeclaracionEstados(idsDeclaraciones);
            var programacion = rp.Consultar<Programacion>();

            return Tuple.Create(declaraciones, personas, subtablas, contactos, dEstados, programacion);
        }
        
    }
}
