using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using ServiceStack;
using ServiceStack.Web;

namespace GestionProyectos.CapaNegocios.Gestores
{

    public class GestorDeclaracionesEstados : GestorBase, IGestorDeclaracionesEstados
    {
                
        public ILogicaValoresEstados LogicaValoresEstados { get; set; }
       
        public QueryResponse<DeclaracionesEstados> Consultar(QueryDeclaracionesEstados query, IRequest request)
        {
            return ConsultarCore(query, request);
        }

        public void Crear(CrearDeclaracionesEstados query, IRequest request)
        {
            var queryDE = new QueryDeclaracionesEstados
            {
                IgnoreMaxLimit = true,
                Fecha_RadicacionGreaterThanOrEqualTo = query.Fecha_RadicacionGreaterThanOrEqualTo,
                Fecha_RadicacionLessThanOrEqualTo = query.Fecha_RadicacionLessThanOrEqualTo
            };
                        
            var refrescarCache = !( query.IgnorarCache.HasValue && query.IgnorarCache.Value);
            RepoHerramienta.Crear(GetRango(queryDE), () => ConsultarCore(queryDE, request, refrescarCache: refrescarCache));
        }

        private QueryResponse<DeclaracionesEstados> ConsultarCore(QueryDeclaracionesEstados query, IRequest request,
            bool refrescarCache=false)
        {           
            var r = new QueryResponse<DeclaracionesEstados>();
            Fabrica.Ejecutar(rp =>
            {
                var estados = GetFromCache(query,  () =>
                {
                    return RepoHerramienta.Consultar(GetRango(query), () =>
                    {
                        var tablas = CargarTablasRango(rp, query);
                        var de= FbContructores.ContructorDeclaracionesEstados(LogicaValoresEstados,tablas).Data;
                        return new QueryResponse<DeclaracionesEstados> { Results = de , Total= de.Count};

                    });
                }, refrescarCache);
                r = QueryResponseFactory.Consultar(query, request, estados.Results, ignoreMaxLimit: query.IgnoreMaxLimit.HasValue && query.IgnoreMaxLimit.Value);
            });

            return r;
        }


        
        
    }
}
