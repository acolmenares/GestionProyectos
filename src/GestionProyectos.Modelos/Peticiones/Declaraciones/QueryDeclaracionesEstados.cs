using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using System;

namespace GestionProyectos.Modelos.Peticiones.Declaraciones
{

    public class CrearDeclaracionesEstados: ITengoFechaRadicacionDesdeHasta
    {
        public DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        public DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
        public bool? IgnorarCache { get; set; }
    }

    public class QueryDeclaracionesEstados : QueryDataDeclaracion<DeclaracionesEstados>
    {
        // poner aqui lo especifico si es necesario     para Filtrar DeclarcionesEstados
    }

    public class DeclaracionesEstados : DeclaracionBase
    {
        public string Elegibilidad { get; set; }
        public DateTime? FechaElegibilidad { get; set; }
        public string Contactado { get; set; }
        public DateTime? FechaContactado { get; set; }
        public string Programado { get; set; }
        public DateTime? FechaProgramado { get; set; }
        public string Reprogramado { get; set; }
        public DateTime? FechaReprogramado { get; set; }
        public string Atendido { get; set; }
        public string MotivoNoAtencion { get; set; }
        public string TipoReprogramacion { get; set; }
        public string AsistioSegundaEntrega { get; set; }
        public int? MotivoNoAtencionId { get; set; }
    }

    public abstract class DeclaracionBase
    {
        public int Id { get; set; }
        public DateTime? FechaRadicacion { get; set; }
        public DateTime? FechaDesplazamiento { get; set; }
        public DateTime? FechaDeclaracion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public DateTime? FechaSegundaEntrega { get; set; }
        public string Declaracion { get; set; }
        public string Horario { get; set; }
        public string Grupo { get; set; }
        public string Fuente { get; set; }
        public string Regional { get; set; }
        public string MunicipioAtencion { get; set; }
        public string EnLinea { get; set; }
        public string TipoDeclarante { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Identificacion { get; set; }
        public string TI { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public int TFE { get; set; }
        public int? TFR { get; set; }
    }

    public abstract class QueryDataDeclaracion<T> : QueryData<T>, ITengoFechaRadicacionDesdeHasta
    {
        public virtual DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        public virtual DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
        public virtual bool? IgnoreMaxLimit { get; set; }
    }

    
}
// Todo Esto de abajo fue un experimiento
/*
public class ConsultarDeclaracionVista: QueryDb<DeclaracionVista>
{
}


public class ConsultarDeclaraciones: QueryDb<Declaracion>
{

}
*/


/*
public abstract class ConsultaDeclaracion : ConsultaDeclaracion<Declaracion>
{
}

public abstract class ConsultaDeclaracion<Into> : Consulta<Declaracion, Into>
{
    public virtual DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
    public virtual DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
    //public virtual DateTime? Fecha_ValoracionGreaterThanOrEqualTo { get; set; }
    //public virtual DateTime? Fecha_ValoracionLessThanOrEqualTo { get; set; }
    //public virtual bool? Fecha_ValoracionIsNull { get; set; }
    //public virtual bool? Fecha_ValoracionNotNull { get; set; }
    //public string Numero_Declaracion { get; set; }
    //public int? Id_lugar_fuente { get; set; }
    //public int[] Id_lugar_fuenteIn { get; set; }
    //public int? Tipo_Declaracion { get; set; }   
}

public class ConsultarDeclaracionEstados : ConsultaDeclaracion<ConsultarDeclaracionEstadosResponse>
{
    // poner aqui los otro criterio de busqueda concretos para estados
}

public class ConsultarDeclaracionEstadosResponse : ConsultaResponse<DeclaracionesEstados>
{
}
*/




