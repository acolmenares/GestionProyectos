using GestionProyectos.Modelos.Peticiones.Declaraciones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IReglasObjetivoDosUno
    {
        ITransformoFechas TransformoFechas {set; }
        DatosObjetivoDosUno DatosObjetivo { set; }
        AnioMesObjetivo AnioMesObjetivo { set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Q1 | Q2 | Q3 | Q4 </returns>
        string Periodo { get; }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>[Enero,Diciembre]</returns>
        string Mes { get; }
        string Regional { get;  }
        string Municipio { get; }

        int Radicados { get; }

        int DobleDeclaracion { get; }
        int SuperoLimite { get;  }
        int NoAsisistioDosProgramaciones { get;  }
        int IncluyoPersonaDeOtroNucleo { get;  }
        int Extemporaneidad { get;  }
        int FueraDeLaCiudad { get;  }
        int NoEsDeplazado { get;  }
        int Masivo { get;  }
        int AtendidoPorOtraFuente { get;  }
        int Intraurbano { get;  }
        int CultivosIlicitos { get;  }
        
        /// <summary>
        /// Clasificados por el motivo de no Atencion 
        /// No se incluye motivo : Programado que No asistio (117)
        /// </summary>
        List<MotivoExclusion> MotivosExclusion { get; }

        /// <summary>
        /// Los que tienen algun Motivo de no Atencion. 
        /// </summary>
        int Excluidos { get; }


        /// <summary>
        /// Los que No tienen  Motivo de no Atencion. 
        /// </summary>
        int Elegibles { get; }

        /// <summary>
        /// Atendidos en el mismo mes de Radicacion 
        /// </summary>
        int AtendidosPrimeraEntregaEnElMesDeRadicacion{ get; }
                
        /// <summary>
        /// Atendidos en un mes posterior al mes de Radicacion (mes analizdo)
        /// y dentro del periodo analizado
        /// </summary>
        int AtendidosPrimeraEntregaEnMesesPosteriores { get; }

        /// <summary>
        /// Atendidos en la  Regional  Municipio con 
        /// y FechaAtencion del mes   analizado 
        /// y FechaRadicacion en el Periodo analizado 
        /// y FechaRadicacion diferente al mes analizado
        /// </summary>
        int AtendidosPrimeraEntregaRadicadosEnMesesAnteriores { get; }


        /// <summary>
        /// Atendidos 
        /// con fecha de radicacion del mes analizado
        /// y fecha de atencion en un periodo posterior al periodo de Radicacion (periodo analizado)
        /// </summary>
        int AtendidosPrimeraEntregaEnPeriodosPosteriores { get; }


        /// <summary>
        /// Atendidos en la  Regional  Municipio  
        /// y FechaAtencion del mes   analizado 
        /// y FechaRadicacion fuera del Periodo analizado 
        /// </summary>
        int AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores { get; }

        /// <summary>
        /// Atendidos en primera entrega
        /// en la  Regional  Municipio  
        /// durante el mes analizado (mes FechaAtencion= mes analizado)
        /// </summary>
        int TotalAtendidosEnPrimeraEntregaEnElMes { get; }

        /// <summary>
        /// Atendidos en primera entrega
        /// en la  Regional  Municipio  
        /// Fecha Atencion > mes analizado
        /// Fecha Radicacion == mes analizado
        /// </summary>
        int TotalAtendidosEnPrimeraEntregaRadicadosEnElMes { get; }

        /// <summary>
        /// Declarantes Atendidos en segunda entrega en el periodo analizado.
        /// </summary>
        int AtendidosEnSegundaEntrega { get; }

        /// <summary>
        /// No Atendidos con Eligibidad en blanco
        /// </summary>
        int PendientePorAplicarFiltros { get; }


        /// <summary>
        /// No Atendido 
        /// Elegibiliad Si 
        /// Contatactdo Si
        /// Programado No o Blank
        /// </summary>
        int PendientePorProgramar { get; }


        /// <summary>
        /// No Atendido 
        /// Elegibiliad Si 
        /// Contatactdo No o Blank
        /// </summary>
        int PendienteNoContactado { get; }

        /// <summary>
        /// No Atendido 
        /// Elegibiliad Si 
        /// Contatactdo Si
        /// Programado para un mes Posterior
        /// </summary>
        int PendienteProgramadoProximoMes { get;  }

        /// <summary>
        /// Programado con Motivo no Atencion : Programado que no asistio (117)
        /// </summary>
        int PendienteProgramadoQueNoAsistio { get; }

        /// <summary>
        /// Total Pendientes de Atender Radicados durante el mes analizado
        /// </summary>
        int TotalPendientesPorAtender { get;  }

        /// <summary>
        /// Exlcluidos 
        /// + Atendidos con mes FechaAtencio= mes FechaRadicacion = mes analizado
        /// + Atendidos en Meses Posteriores al mes analizado
        /// + Atendidos en Periodos Posteriores 
        /// + Pendientes por atender
        /// </summary>
        int SumaComprobacion { get; }


        /// <summary>
        /// Total Atendidos radicados durante el mes analizado
        /// /// sobre
        /// Total Elegibles radicados durante el mes analizado
        /// </summary>
        double PorcentajeAtendidos { get; }
        
        /// <summary>
        /// Elegibles + AtendidosRadicacos en el mes  + Pendientes por Atender) = 0
        /// para los radicados en  el mes analizado
        /// </summary>
        bool SumaDatosAtendidosEsIgual { get; }

        /// <summary>
        /// Suma MotivosExcluios - suma cada motivo por separado =0
        /// </summary>
        bool SumaDatosExlcuidosEsIgual { get; }

        /// <summary>
        ///  SumaComprobacion=Radicados del Mes analizado
        /// </summary>
        bool SumaComprobacionEsIgual { get;  }

    }

  
}
