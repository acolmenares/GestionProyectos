using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface ITransformoFechas
    {
        string ConvertirEnAnioMes(DateTime? value);
        string ConvertirEnPeriodo(DateTime? value);
        string ConvertirEnNombreDeMes(DateTime? value);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>T.NameyyyyMMddyyyyMMdd</returns>
        string ConvertirEnLlave<T>(ITengoFechaRadicacionDesdeHasta query);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>urn:Lave</returns>
        string ConvertirEnURN<T>(ITengoFechaRadicacionDesdeHasta query) ;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>yyyyMMddyyyyMMdd</returns>
        string ConvertirEnRango(ITengoFechaRadicacionDesdeHasta query) ;
        string ConvertirLlaveEnRango(string llave);
        string ConvertirLlaveEnT(string llave);
        string EnAAAAMMDD(DateTime? value);
        string ConvertirRangoEnPeriodo(string rango);
        string ConvertirRangoEnAnioMes(string rango);
    }

    public interface ITengoFechaRadicacionDesdeHasta
    {
        DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
    }
   
}

