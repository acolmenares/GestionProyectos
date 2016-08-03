using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using System;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class TransformoFechas : ITransformoFechas
    {

        string[] meses = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        string formatoFecha = "yyyyMM";

        public string ConvertirEnAnioMes(DateTime? value)
        {
            return value.HasValue ? value.Value.ToString(formatoFecha) : null;
        }

        public string ConvertirEnPeriodo(DateTime? value)
        {
            return string.Format("Q{0}", value.HasValue ? ((((value.Value.Month + 3) % 12) + 2) / 3).ToString() : "");
        }

        public string ConvertirEnNombreDeMes(DateTime? value)
        {
            return value.HasValue ? meses[value.Value.Month-1] : null;
        }

        public string ConvertirEnLlave<T>(T query) where T : ITengoFechaRadicacionDesdeHasta
        {
            return "{0}{1}{2}".Fmt(typeof(T).Name,
                FormatDate(query.Fecha_RadicacionGreaterThanOrEqualTo.HasValue ? query.Fecha_RadicacionGreaterThanOrEqualTo.Value : DateTime.MinValue),
                FormatDate(query.Fecha_RadicacionLessThanOrEqualTo.HasValue ? query.Fecha_RadicacionLessThanOrEqualTo.Value : DateTime.Now.AddDays(1)));
        }

        public string ConvertirEnURN<T>(T query) where T : ITengoFechaRadicacionDesdeHasta
        {
            return "urn:{0}".Fmt(ConvertirEnLlave(query));
        }


        protected  string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
