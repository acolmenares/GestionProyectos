using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Web;
using System.Collections.Generic;
using System.Linq;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorObjetivoDosUno : GestorBase, IGestorObjetivoDosUno
    {
        public IGestorDeclaracionesEstados GestorDeclaracionesEstados { get; set; }
        public IReglasObjetivoDosUno ReglasObjetivo { get; set; }


        public QueryResponse<ObejtivoDosUno> Consultar(QueryObjetivoDosUno query, IRequest request)
        {
            var de = GestorDeclaracionesEstados.Consultar(new QueryDeclaracionesEstados
            {
                IgnoreMaxLimit=true,
                Fecha_RadicacionGreaterThanOrEqualTo= query.Fecha_RadicacionGreaterThanOrEqualTo,
                Fecha_RadicacionLessThanOrEqualTo= query.Fecha_RadicacionLessThanOrEqualTo
            }, request);
                        
            var dobjetivo = FbDatosObjectivos.DatosObjetivoDosUno(de.Results, TransformoFechas);

            var data =FbContructores.ConstructorObjetivoDosUno(dobjetivo, ReglasObjetivo, TransformoFechas).Data;

            var regionales = data.GroupBy(q => q.Regional).Select(regional => new
            {
                Nombre = regional.Key,
                Municipios = regional.GroupBy(m => m.Municipio).Select(municipio => new
                {
                    Nombre = municipio.Key,
                    Periodos = municipio.GroupBy(pr => pr.Periodo).Select(periodo => new
                    {
                        Regional = regional.Key,
                        Municipio = municipio.Key,
                        Nombre = periodo.Key,
                        Data = periodo.Select( p=> data.IndexOf(p)).ToList(),
                    }).ToList()
                }).ToList()
            }).ToList();

            var periodos = data.GroupBy(q=> q.Periodo).Select(periodo=> new {
                Nombre= periodo.Key,
                Meses = periodo.GroupBy(ms=>ms.Mes).Select(mes=> new {
                    Periodo= periodo.Key,
                    Nombre = mes.Key,
                    Regionales = mes.GroupBy(rg=>rg.Regional).Select(regional=> new {
                        Periodo=periodo.Key,
                        Mes = mes.Key,
                        Nombre = regional.Key,
                        Data = regional.Select(p=> data.IndexOf(p)).ToList()
                    }).ToList()
                }).ToList()
            }).ToList();


            var municipios = data.GroupBy(m => m.Municipio).Select(municipio => new
            {
                Nombre= municipio.Key,
                Periodos = municipio.GroupBy(pr=>pr.Periodo).Select(periodo=> new {
                    Municipio= municipio.Key,
                    Nombre= periodo.Key,
                    Meses = periodo.GroupBy(ms=>ms.Mes).Select(mes=> new {
                        Municipio= municipio.Key,
                        Periodo = periodo.Key,
                        Nombre = mes.Key,
                        Data = mes.Select( p=> data.IndexOf(p)).ToList()

                    }).ToList()
                }).ToList()
            }).ToList();

            var r = new QueryResponse<ObejtivoDosUno> { Results = data, Total = data.Count};
            r.Meta = new Dictionary<string, string>();

            r.Meta.Add("Regionales", JsonSerializer.SerializeToString( regionales));
            r.Meta.Add("Periodos",  JsonSerializer.SerializeToString(periodos));
            r.Meta.Add("Municipios", JsonSerializer.SerializeToString(municipios));

            return r;
        }
    }
}
/*
 * Regionales : Regional -> Municipios -> Periodos -> Datos de Cada Periodo = cada registro corresponde a un  dato por mes 
 * Periodos :   Periodo  -> Meses -> Regionales -> datos de cada regional = cada registro corresponde un dato por municipio
 * 
 * 
*/
/*
 var regionales = data.GroupBy(q => q.Regional).Select(regional => new
            {
                Nombre = regional.Key,
                Municipios = regional.GroupBy(m => m.Municipio).Select(municipio => new
                {
                    Nombre = municipio.Key,
                    Periodos = municipio.GroupBy(pr => pr.Periodo).ToDictionary(x => x.Key, x => x.ToList())  //.ToList()
                }).ToDictionary(y => y.Nombre, y => y.Periodos)
            }).ToDictionary(z => z.Nombre, z => z.Municipios);

            var r = new QueryObjetivoDosUnoResponse { Results = data, Total = data.Count, Regionales= regionales};
            //r.Meta = new Dictionary<string, string>();

            //r.Meta.Add("Regionales", JsonSerializer.SerializeToString( regionales));

            return r;

 */
