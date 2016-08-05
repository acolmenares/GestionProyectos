using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Peticiones.Objetivos
{
    public class CrearRango: Crear,  ITengoFechaRadicacionDesdeHasta
    {
        public DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        public DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
    }

    public class ConsultarRango: Consultar<Rango>
    {
                
    }

    public class BorrarRango : Borrar, ITengoFechaRadicacionDesdeHasta
    {
        public DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        public DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
    }


    public class Rango
    {
        /// <summary>
        /// yyyyMMddyyyyMMdd DesdeHasta
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Ultimo Periodo del Rango : Q1 || Q2 || Q3 || Q4
        /// </summary>
        public string Periodo { get; set; }
        /// <summary>
        /// Ultimo AñoMes del Rango yyyyMM
        /// </summary>
        public string AnioMes { get; set; }
        
        public List<RegionalObjetivo> Regionales { get; set; }
        public List<string> Municipios { get; set; }

        public string Texto { get { return string.Format("{0} {1}", Periodo, AnioMes); } }
 
    }
   
}
