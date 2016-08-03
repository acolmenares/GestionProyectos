using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class ConstructorObjetivoDosUno : IConstructorObjetivoDosUno
    {
        public DatosObjetivoDosUno DatosObjetivo { get; set; }
        public IReglasObjetivoDosUno Reglas { get; set; }
        public ITransformoFechas TransformoFechas { get; set; }
        
        public List<ObejtivoDosUno> Data
        {
            get
            {
                
                var r = new List<ObejtivoDosUno>();

                Reglas.DatosObjetivo = DatosObjetivo;
                Reglas.TransformoFechas = TransformoFechas;
                               
                DatosObjetivo.Regionales.ForEach(regional => {
                    regional.Municipios.ForEach(municipio => {
                        
                        municipio.AniosMeses.ForEach(animomes => {
                            Reglas.AnioMesObjetivo = animomes;
                            var objetivo = new ObejtivoDosUno();
                            objetivo.PopulateWith(Reglas);
                            r.Add(objetivo);
                        });
                    });
                });

                return r;
            }
        }
    }
}
