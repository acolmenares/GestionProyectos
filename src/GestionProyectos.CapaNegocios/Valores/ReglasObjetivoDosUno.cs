using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class ReglasObjetivoDosUno : IReglasObjetivoDosUno
    {
        public ITransformoFechas TransformoFechas { get; set; }

        private DatosObjetivoDosUno datosObjetivo;
        private AnioMesObjetivo aniomes;
        private List<DeclaracionesEstados> estados;

        public string Regional { get { return aniomes.Regional; } }
        public string Municipio { get { return aniomes.Municipio; } }

        public int ProgramadoQueNoAsistioId { get; set; }
        public int DobleDeclaracionId { get; set; }
        public int SuperoLimiteId { get; set; }
        public int NoUbicadoId { get; set; }
        public int NoAsisistioDosProgramacionesId { get; set; }
        public int IncluyoPersonaDeOtroNucleoId { get; set; }
        public int ExtemporaneidadId { get; set; }
        public int FueraDeLaCiudadId { get; set; }
        public int NoEsDeplazadoId { get; set; }
        public int MasivoId { get; set; }
        public int AtendidoPorOtraFuenteId { get; set; }
        public int IntraurbanoId { get; set; }
        public int CultivosIlicitosId { get; set; }

        //public List<int> MotivoNoAtencionIds { get; set; }

        public ReglasObjetivoDosUno()
        {
            datosObjetivo = new DatosObjetivoDosUno();
            aniomes = new AnioMesObjetivo();
            estados = new List<DeclaracionesEstados>();
            TransformoFechas = new TransformoFechas(); // por defecto pero puede ser cambiado !!!

            ProgramadoQueNoAsistioId = 1117;
            DobleDeclaracionId = 376;
            SuperoLimiteId = 4547;
            NoUbicadoId = 10;
            NoAsisistioDosProgramacionesId = 4548;
            IncluyoPersonaDeOtroNucleoId = 380;
            ExtemporaneidadId = 377;
            FueraDeLaCiudadId = 1123;
            NoEsDeplazadoId = 12;
            MasivoId = 3729;
            AtendidoPorOtraFuenteId = 33;
            IntraurbanoId = 3405;
            CultivosIlicitosId = 1120;

            //MotivoNoAtencionIds = new List<int>(new int[]  { 376, 4547, 4548, 380, 377, 1123, 12, 3729, 33, 3405, 1120  });

        }

       

        public DatosObjetivoDosUno DatosObjetivo
        {
            get { return datosObjetivo; }
            set
            {
                datosObjetivo = value;
                LlenarEstados();
            }
        }


        public AnioMesObjetivo AnioMesObjetivo
        {
            get { return AnioMesObjetivo; }
            set
            {
                aniomes = value;
                LlenarEstados();

            }
        }

        public string Periodo
        {
            get
            {
                int test = 0;
                return (int.TryParse(aniomes.Nombre, out test) && aniomes.Nombre.Length == 6) ?
                 TransformoFechas.ConvertirEnPeriodo(new DateTime(int.Parse(aniomes.Nombre.Substring(0, 4)), int.Parse(aniomes.Nombre.Substring(4, 2)), 1)) :
                 TransformoFechas.ConvertirEnPeriodo(null);

            }
        }

        public string Mes
        {
            get
            {
                int test = 0;
                return (int.TryParse(aniomes.Nombre, out test) && aniomes.Nombre.Length == 6) ?
                 TransformoFechas.ConvertirEnNombreDeMes(new DateTime(int.Parse(aniomes.Nombre.Substring(0, 4)), int.Parse(aniomes.Nombre.Substring(4, 2)), 1)) :
                 TransformoFechas.ConvertirEnNombreDeMes(null);
            }
        }

        public int Radicados
        {
            get
            {
                return estados.Count();
            }
        }

        public int DobleDeclaracion
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == DobleDeclaracionId);
            }
        }

        public int SuperoLimite
        {
            get
            {
                return estados.Count(q => !Atendido(q) && 
                ( 
                  q.MotivoNoAtencionId == SuperoLimiteId
                  || q.MotivoNoAtencionId== NoUbicadoId
                ));
            }
        }

        public int NoAsisistioDosProgramaciones
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId ==NoAsisistioDosProgramacionesId);
            }
        }

        public int IncluyoPersonaDeOtroNucleo
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == IncluyoPersonaDeOtroNucleoId);
            }
        }
        public int Extemporaneidad
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == ExtemporaneidadId);
            }
        }
        public int FueraDeLaCiudad
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == FueraDeLaCiudadId);
            }
        }
        public int NoEsDeplazado
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == NoEsDeplazadoId);
            }
        }

        public int Masivo
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == MasivoId);
            }
        }
        public int AtendidoPorOtraFuente
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == AtendidoPorOtraFuenteId);
            }
        }

        public int Intraurbano
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == IntraurbanoId);
            }
        }
        public int CultivosIlicitos
        {
            get
            {
                return estados.Count(q => !Atendido(q) && q.MotivoNoAtencionId == CultivosIlicitosId);
            }
        }


        public List<MotivoExclusion> MotivosExclusion
        {
            get
            {
                return estados.Where(f => !Atendido(f) && f.MotivoNoAtencionId.HasValue && f.MotivoNoAtencionId!=ProgramadoQueNoAsistioId )
                    .GroupBy(f => f.MotivoNoAtencion)
                    .Select(x => new MotivoExclusion { Id=x.First().MotivoNoAtencionId.Value,  Motivo = x.Key, Cantidad = x.Count() }).ToList();
            }
        }

        public int Excluidos
        {
            get
            {
                return MotivosExclusion.Sum(f=>f.Cantidad);
            }
        }

        public int Elegibles
        {
            get
            {
                return Radicados - Excluidos;
            }
        }

        public int AtendidosPrimeraEntregaEnElMesDeRadicacion
        {
            get
            {
                return estados.Count(q => Atendido(q) && AnioMes(q.FechaAtencion) == AnioMes(q.FechaRadicacion));
            }
        }

        public int AtendidosPrimeraEntregaEnMesesPosteriores
        {
            get
            {
                return estados.Count(q => Atendido(q) && AnioMes(q.FechaAtencion) != AnioMes(q.FechaRadicacion)
                && ConvertirEnPerido(q.FechaAtencion) == ConvertirEnPerido(q.FechaRadicacion));
            }
        }

        public int AtendidosPrimeraEntregaRadicadosEnMesesAnteriores
        {
            get
            {                
                return datosObjetivo.Datos.Count(q => Atendido(q) && q.Regional == aniomes.Regional && q.MunicipioAtencion == aniomes.Municipio 
                    && AnioMes(q.FechaAtencion)== aniomes.Nombre && ConvertirEnPerido(q.FechaRadicacion)== Periodo  && AnioMes(q.FechaRadicacion) != aniomes.Nombre);
            }
        }

        
        public int AtendidosPrimeraEntregaEnPeriodosPosteriores
        {
            get
            {
                return estados.Count( q=> Atendido(q) &&  ConvertirEnPerido(q.FechaAtencion) != ConvertirEnPerido(q.FechaRadicacion));
            }

        }
       
        public int AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores
        {
            get
            {
                return datosObjetivo.Datos.Count(q => Atendido(q) && q.Regional == aniomes.Regional && q.MunicipioAtencion == aniomes.Municipio
                    && AnioMes(q.FechaAtencion) == aniomes.Nombre && ConvertirEnPerido(q.FechaRadicacion) != Periodo );
            }
        }

        public int TotalAtendidosEnPrimeraEntregaEnElMes
        {
            get
            {
                return AtendidosPrimeraEntregaEnElMesDeRadicacion
                    + AtendidosPrimeraEntregaRadicadosEnMesesAnteriores
                    + AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores;
            }
        }

        public int TotalAtendidosEnPrimeraEntregaRadicadosEnElMes
        {
            get
            {
                return AtendidosPrimeraEntregaEnElMesDeRadicacion +
                    AtendidosPrimeraEntregaEnMesesPosteriores +
                    AtendidosPrimeraEntregaEnPeriodosPosteriores;
            }
        }

        public int AtendidosEnSegundaEntrega
        {
            get
            {
                return
                    datosObjetivo.Datos.Count(q =>
                      Atendido(q) && q.Regional == aniomes.Regional && q.MunicipioAtencion == aniomes.Municipio
                      && q.AsistioSegundaEntrega == "Si" && AnioMes(q.FechaSegundaEntrega) == aniomes.Nombre);
            }
        }

        public int PendientePorAplicarFiltros
        {
            get
            {
                return estados.Count(q => !Atendido(q) && string.IsNullOrEmpty(q.Elegibilidad));
            }
        }

        public int PendientePorProgramar
        {
            get
            {
                return estados.Count(q => !Atendido(q) 
                                       && q.Elegibilidad=="Si" 
                                       && q.Contactado=="Si" 
                                       && q.Programado!="Si");
            }
        }

        public int PendienteNoContactado
        {
            get
            {
                return estados.Count(q => !Atendido(q) 
                                       && q.Elegibilidad == "Si" 
                                       && q.Contactado != "Si" );
            }
        }

        public int PendienteProgramadoProximoMes
        {
            get
            {
                return estados.Count(q => !Atendido(q) 
                                       && q.Elegibilidad == "Si" 
                                       && q.Contactado == "Si" 
                                       && q.Programado == "Si"  
                                       && AnioMes(q.FechaProgramado)!= aniomes.Nombre);
            }
        }

        public int PendienteProgramadoQueNoAsistio
        {
            get
            {
                return estados.Count(q => !Atendido(q)
                                       && q.Elegibilidad == "Si"
                                       && q.Contactado == "Si"
                                       && q.Programado == "Si"
                                       && q.MotivoNoAtencionId == ProgramadoQueNoAsistioId);
            }
        }

        public int TotalPendientesPorAtender
        {
            get
            {
                return PendienteNoContactado
                    + PendientePorAplicarFiltros
                    + PendientePorProgramar
                    + PendienteProgramadoProximoMes
                    + PendienteProgramadoQueNoAsistio;
            }
        }
                
        public int SumaComprobacion
        {
            get
            {
                return Excluidos
                    + AtendidosPrimeraEntregaEnElMesDeRadicacion
                    + AtendidosPrimeraEntregaEnMesesPosteriores
                    + AtendidosPrimeraEntregaEnPeriodosPosteriores
                    + TotalPendientesPorAtender;
            }
        }

        public double PorcentajeAtendidos {
            get
            {
                return
                    Elegibles > 0?
                    (double) TotalAtendidosEnPrimeraEntregaRadicadosEnElMes / (double)Elegibles:
                    0;
            }
        }
        
        public bool SumaDatosAtendidosEsIgual
        {
            get
            {
                return Elegibles - (TotalAtendidosEnPrimeraEntregaRadicadosEnElMes + TotalPendientesPorAtender) == 0;
            }
        }

        
        public bool SumaDatosExlcuidosEsIgual
        {
            get
            {
                return Excluidos - (
                      DobleDeclaracion
                    + SuperoLimite
                    + NoAsisistioDosProgramaciones
                    + IncluyoPersonaDeOtroNucleo
                    + Extemporaneidad
                    + FueraDeLaCiudad
                    + NoEsDeplazado 
                    + Masivo 
                    + AtendidoPorOtraFuente 
                    + Intraurbano
                    + CultivosIlicitos 
                    ) == 0;
            }
        }

        public bool SumaComprobacionEsIgual { get { return Radicados - SumaComprobacion == 0; } }

        private string ConvertirEnPerido(DateTime? fecha)
        {
            return TransformoFechas.ConvertirEnPeriodo(fecha);
        }

        private void LlenarEstados()
        {
            if (datosObjetivo == null || aniomes == null)
            {
                estados = new List<DeclaracionesEstados>();
            }
            else
            {
                estados = datosObjetivo.Datos
                    .Where(q => q.Regional == aniomes.Regional 
                             && q.MunicipioAtencion == aniomes.Municipio 
                             && AnioMes(q.FechaRadicacion) == aniomes.Nombre).ToList();
            }
        }




        private bool Atendido(DeclaracionesEstados dato)
        {
            return string.IsNullOrEmpty(dato.MotivoNoAtencion) && dato.FechaAtencion.HasValue;
        }

        private string AnioMes(DateTime? fecha)
        {
            return TransformoFechas.ConvertirEnAnioMes(fecha);
        }


        /*
        private bool DatoEsDelAnioMes (DeclaracionesEstados dato, AnioMesObjetivo aniomes)
        {
            return dato.Regional == aniomes.Regional && dato.MunicipioAtencion == aniomes.Municipio && AnioMes(dato.FechaRadicacion) == aniomes.Nombre;
        }

        private string AnioMesRadicacion(DeclaracionesEstados dato)
        {
            return AnioMes(dato.FechaRadicacion);
        }

        private string AnioMesAtencion(DeclaracionesEstados dato)
        {
            return AnioMes(dato.FechaAtencion);
        }
        
        */

    }
}
