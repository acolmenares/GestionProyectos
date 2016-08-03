using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using ServiceStack;

namespace GestionProyectos.Servicios
{
    public class ServicioDeclaracionesEstados : Service
    {
        public IGestorDeclaracionesEstados Gestor { get; set; }

        public QueryResponse<DeclaracionesEstados> Get(QueryDeclaracionesEstados query)
        {
            QueryResponse<DeclaracionesEstados> r = Gestor.Consultar(query, Request);
            return r;
        }

        public void Any (CrearDeclaracionesEstados query)
        {
            
            Gestor.Crear(query, Request);
        }

    }
}
        /*

        public ConsultarDeclaracionEstadosResponse Get (ConsultarDeclaracionEstados modelo)
        {    
            return Gestor.Cosultar(modelo, Request);
        }

       
        */
        //ContentType:"application/vnd.ms-excel"
        //VCardContentType = "text/x-vcard"
        //http://localhost:49884/gp-api/html/reply/QueryDeclaracionEstados?Fecha_RadicacionGreaterThanOrEqualTo=20141001&Fecha_RadicacionLessThanOrEqualTo=20160630&Tipo_Declaracion=921&PrimerApellido=RUIZ

        /*
       
        return base.Request.ToOptimizedResultUsingCache(this.Cache, 
                "urn:customers", () =>
                    this.ResolveService<CustomersService>()
                    .Get(new Customers()));


        public QueryResponse<DeclaracionVista> Get(ConsultarDeclaracionVista query)
        {

            Request.ToOptimizedResultUsingCache(this.Cache, "", () => { return new Personas(); });
            //Console.WriteLine(this.Response.ToString());
            return Gestor.Consultar(query, Request);

        }

        public QueryResponse<Declaracion> Get( ConsultarDeclaraciones query)
        {
            return Gestor.Consultar(query, Request);
        }
        */
