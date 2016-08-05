using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System.Collections.Generic;
using System.Linq;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class FabricaDatosObjetivos : IFabricaDatosObjetivos
    {
        public DatosObjetivoDosUno DatosObjetivoDosUno(List<DeclaracionesEstados> estados, ITransformoFechas vo)
        {

            var e = estados.Where(q => q.TipoDeclarante == "Desplazado").ToList();
            return new DatosObjetivoDosUno
            {
                Datos = e,
                Regionales = e.GroupBy(rg => rg.Regional).Select(regional => new RegionalObjetivo
                {
                    Nombre = regional.Key,
                    Municipios = regional.GroupBy(mn => mn.MunicipioAtencion).Select(municipio => new MunicipioObjectivo
                    {
                        Regional = regional.Key,
                        Nombre = municipio.Key,
                        AniosMesesRadicacion = municipio.GroupBy(per => vo.ConvertirEnAnioMes(per.FechaRadicacion)).Select(mes => new  AnioMesObjetivo
                        {
                            Regional = regional.Key,
                            Municipio = municipio.Key,
                            Nombre = mes.Key,
                        }).OrderBy(x => x.Nombre).ToList(),
                        AniosMesesAtencion= municipio.GroupBy(per => vo.ConvertirEnAnioMes(per.FechaAtencion)).Select(mes => new AnioMesObjetivo
                        {
                            Regional = regional.Key,
                            Municipio = municipio.Key,
                            Nombre = mes.Key,
                        }).OrderBy(x => x.Nombre).ToList()
                    }).OrderBy(x => x.Nombre).ToList()
                }).OrderBy(x => x.Nombre).ToList()
            };
        }

        
    }
}
