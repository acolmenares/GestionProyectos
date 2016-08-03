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
        string ConvertirEnLlave<T>(T query) where T : ITengoFechaRadicacionDesdeHasta;
        string ConvertirEnURN<T>(T query) where T: ITengoFechaRadicacionDesdeHasta;
    }

    public interface ITengoFechaRadicacionDesdeHasta
    {
        DateTime? Fecha_RadicacionGreaterThanOrEqualTo { get; set; }
        DateTime? Fecha_RadicacionLessThanOrEqualTo { get; set; }
    }
   
}

