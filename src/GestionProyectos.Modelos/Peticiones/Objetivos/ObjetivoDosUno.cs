using GestionProyectos.Modelos.Peticiones.Declaraciones;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Objetivos
{
        
    public class QueryObjetivoDosUno :QueryObjetivo<ObejtivoDosUno>
    {
        
    }

    public class QueryObjetivoDosUnoResponse: QueryResponse<ObejtivoDosUno>
    {
        public Dictionary<string, Dictionary<string,Dictionary<string,List<ObejtivoDosUno>>>>  Regionales { get; set; }
        public QueryObjetivoDosUnoResponse()
        {
            //var periodos = new Dictionary<string, List<ObejtivoDosUno>>();
            //var municipios = new Dictionary<string, Dictionary<string, List<ObejtivoDosUno>>>();
            Regionales = new Dictionary<string, Dictionary<string, Dictionary<string, List<ObejtivoDosUno>>>>();

        }
    }

    public abstract class QueryObjetivo<T> : QueryDataDeclaracion<T>
    {
        public string Periodo { get; set; }
        public string Regional { get; set; }
        public string Municipio { get; set; }
        public string Mes { get; set; }
    }

    // esta es la respuesta que quiero enviar !
    public class ObejtivoDosUno
    {
        public ObejtivoDosUno()
        {
            MotivosExclusion = new List<MotivoExclusion>();
        }
        public string Periodo { get; set; }
        public string Regional { get; set; }
        public string Municipio { get; set; }
        public string Mes { get; set; }
        public int Radicados { get; set; }

        public int DobleDeclaracion { get; set; }
        public int SuperoLimite { get; set; }
        public int NoAsisistioDosProgramaciones { get; set; }
        public int IncluyoPersonaDeOtroNucleo { get; set; }
        public int Extemporaneidad { get; set; }
        public int FueraDeLaCiudad { get; set; }
        public int NoEsDeplazado { get; set; }
        public int Masivo { get; set; }
        public int AtendidoPorOtraFuente { get; set; }
        public int Intraurbano { get; set; }
        public int CultivosIlicitos { get; set; }
        
        public List<MotivoExclusion> MotivosExclusion { get; set; }
        public int Excluidos { get; set; }
        public int Elegibles { get; set; }

        public int AtendidosPrimeraEntregaEnElMesDeRadicacion { get; set; }
        public int AtendidosPrimeraEntregaEnMesesPosteriores { get; set; }
        public int AtendidosPrimeraEntregaRadicadosEnMesesAnteriores { get; set; }
        public int AtendidosPrimeraEntregaEnPeriodosPosteriores { get; set; }
        public int AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores { get; set; }
        public int TotalAtendidosEnPrimeraEntregaEnElMes { get; set; }
        public int TotalAtendidosEnPrimeraEntregaRadicadosEnElMes { get; set; }
        public int AtendidosEnSegundaEntrega { get; set; }

        public int PendientePorAplicarFiltros { get; set; }
        public int PendientePorProgramar { get; set; }
        public int PendienteNoContactado { get; set; }
        public int PendienteProgramadoProximoMes { get; set; }
        public int PendienteProgramadoQueNoAsistio { get; set; }
        public int TotalPendientesPorAtender { get; set; }
        public int SumaComprobacion { get; set; }
        public double PorcentajeAtendidos { get; set; }
        public bool SumaDatosAtendidosEsIgual{ get; set; }
        public bool SumaDatosExlcuidosEsIgual { get; set; }
        public bool SumaComprobacionEsIgual { get; set; }

    }

    public class MotivoExclusion
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public int Cantidad { get; set; }
    }

    /// <summary>
    /// Clase de Apoyo para gnerar ObjetivoDosUno
    /// </summary>
    /// 
    public abstract class DatosObjetivo<T>
    {
        public virtual List<T> Datos { get; set; }
        public virtual List<RegionalObjetivo> Regionales { get; set; }
    }

    public class DatosObjetivoDosUno:DatosObjetivo<DeclaracionesEstados>
    {                
    }

    public class RegionalObjetivo
    {
        public string Nombre { get; set; }
        public List<MunicipioObjectivo> Municipios { get; set; }
    }
    
    public class MunicipioObjectivo
    {
        public string Regional { get; set; }
        public string Nombre { get; set; }
        public List<AnioMesObjetivo> AniosMeses { get; set; }
    }
    public class AnioMesObjetivo
    {
        public string Regional { get; set; }
        public string Municipio { get; set; }
        public string Nombre { get; set; }
    }


}
