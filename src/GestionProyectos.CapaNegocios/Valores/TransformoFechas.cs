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
            return value.HasValue ? meses[value.Value.Month - 1] : null;
        }

        public string ConvertirEnLlave<T>(ITengoFechaRadicacionDesdeHasta query) 
        {
            var type = typeof(T);
            var name = type.Name.StartsWith("QueryResponse`") ? "Query" + type.GetGenericArguments()[0].Name :
                type.Name.StartsWith("List`")? type.GetGenericArguments()[0].Name:
                type.Name;

            return "{0}{1}".Fmt(name, ConvertirEnRango(query));
                
        }

        public string ConvertirEnURN<T>(ITengoFechaRadicacionDesdeHasta query) 
        {
            return "urn:{0}".Fmt(ConvertirEnLlave<T>(query));
        }


        public string ConvertirEnRango(ITengoFechaRadicacionDesdeHasta query) 
        {
            return "{0}{1}".Fmt(
                FormatDate(query.Fecha_RadicacionGreaterThanOrEqualTo.HasValue ? query.Fecha_RadicacionGreaterThanOrEqualTo.Value : DateTime.MinValue),
                FormatDate(query.Fecha_RadicacionLessThanOrEqualTo.HasValue ? query.Fecha_RadicacionLessThanOrEqualTo.Value : DateTime.Now.AddDays(1)));
        }

        public string ConvertirLlaveEnRango(string llave)
        {
            return llave.Substring(llave.Length - 16);
        }

        public string ConvertirLlaveEnT(string llave)
        {
            return llave.Substring(0, llave.Length - 16);
        }

        public string ConvertirRangoEnPeriodo(string rango)
        {
            
            return ConvertirEnPeriodo( ConvertirEnFecha(rango.Substring(8)));

        }

        public string ConvertirRangoEnAnioMes(string rango)
        {
            return ConvertirEnAnioMes(ConvertirEnFecha(rango.Substring(8)));
        }

        

        public string EnAAAAMMDD(DateTime? value)
        {
            return value.HasValue? FormatDate(value.Value): "";
        }

        protected  string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

        private DateTime? ConvertirEnFecha(string parcial)
        {
            var sanio = parcial.Substring(0, 4);
            var smes = parcial.Substring(4, 2);
            var sdia = parcial.Substring(6, 2);

            int anio = 0;
            int mes = 0;
            int dia = 0;


            if (int.TryParse(sanio, out anio) && int.TryParse(smes, out mes) && int.TryParse(sdia, out dia))
            {
                return new DateTime(anio, mes, dia);

            }

            return null;
        }


        protected Tuple<DateTime?, DateTime?> ConvertirRangoEnFechas(string rango)
        {            
            return Tuple.Create(ConvertirEnFecha(rango.Substring(0, 8)), ConvertirEnFecha(rango.Substring(8, 0)));
        }

    }
}
