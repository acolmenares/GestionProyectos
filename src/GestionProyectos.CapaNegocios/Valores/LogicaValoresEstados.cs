using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class LogicaValoresEstados : LogicaValoresComun, ILogicaValoresEstados, ILogicaValoresComun
    {
        private readonly int elegible = 4036;
        private readonly int contactado = 4037;
        private readonly int programado = 4038;
        private readonly int reprogramado = 4039;
        private readonly int primeraEntrega = 72;
        private readonly int segundaEntrega = 918;
        private readonly int valorNo = 20;
        //private readonly int valorSi = 19;

        private readonly int programadoQueNoAsistioId = 1117;

        public virtual string EsElegible(List<DeclaracionEstados> estados, List<SubTablas> subtablas, Declaracion declaracion)
        {
            var estado = ObtenerDeclaracionEstado(estados, declaracion, elegible);

            var descripcion= ObtenerDescripcionEstado(subtablas, estado);

            return declaracion.Id_Motivo_Noatender.HasValue && declaracion.Id_Motivo_Noatender != programadoQueNoAsistioId ? "No" : descripcion;
        }



        public virtual DeclaracionEstados ObtenerEstadoElegibilidad(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, elegible);
        }

        public virtual DeclaracionEstados ObtenerEstadoContactado(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, contactado);
        }

        public virtual DeclaracionEstados ObtenerEstadoProgramado(List<SubTablas> subTablas, List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion)
        {
            return ObtenerEstadoEntrega(programacion, declaracionEstados, declaracion, primeraEntrega);

        }

        public virtual DeclaracionEstados ObtenerEstadoReprogramado(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, reprogramado); // TODO : verficar
        }

        
        public virtual string ObtenerTipoReprogramacion(List<SubTablas> subtablas, List<Programacion> programacion, List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            
            var estado=  estados.FindAll(
                q => q.Id_Declaracion == declaracion.Id 
                && (q.Id_Tipo_Estado == programado || q.Id_Tipo_Estado == reprogramado) 
                && q.Id_Asistio ==valorNo)
                .OrderByDescending(q => q.Fecha).ThenByDescending(q => q.Id).Take(1).SingleOrDefault() ?? new DeclaracionEstados();

            var prg = programacion.SingleOrDefault(q => q.Id == estado.Id_Programa) ?? new Programacion();
            
            return ObtenerDescripcion(subtablas, prg.Id_TipoEntrega);
        }

        public DeclaracionEstados ObtenerEstadoSegundaEntrega(List<SubTablas> subTablas, List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion)
        {
            return ObtenerEstadoEntrega(programacion, declaracionEstados, declaracion, segundaEntrega);

        }

        private DeclaracionEstados ObtenerEstadoEntrega(List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion, int tipoEntrega)
        {
            return
            declaracionEstados.Where(q => q.Id_Declaracion == declaracion.Id
                                      && (q.Id_Tipo_Estado == programado || q.Id_Tipo_Estado == reprogramado))
                                       .Join(programacion.Where(x => x.Id_TipoEntrega == tipoEntrega), a => a.Id_Programa, b => b.Id, (a, b) => new DeclaracionEstados
                                      //.Join( programacion, a => (object)new { Programa = a.Id_Programa, TipoEntrega = tipoEntrega }, b => (object)new { Programa = b.Id, TipoEntrega = b.Id_TipoEntrega }, (a, b) => new DeclaracionEstados
                                      {
                                           Id = a.Id,
                                           Fecha = b.Fecha,
                                           Id_Declaracion = a.Id_Declaracion,
                                           Id_Tipo_Estado = a.Id_Tipo_Estado,
                                           Id_Como_Estado = a.Id_Como_Estado,
                                           Id_Programa = a.Id_Programa,
                                           Id_Asistio = a.Id_Asistio

                                       })
                                      .OrderByDescending(f => f.Fecha).ThenByDescending(f => f.Id).Take(1).SingleOrDefault() ?? new DeclaracionEstados();
        }
    }
}

/*
  select top 1 Declaracion_estados.Id 
     from Declaracion_Estados 
	    join Programacion pr on pr.Id=Declaracion_Estados.Id_Programa and pr.Id_TipoEntrega=@SegundaEntrega
     where Declaracion_Estados.Id_Declaracion=Declaracion.Id
     and  Declaracion_Estados.Id_Tipo_Estado in (@Id_Programado, @Id_ReProgramado) 
	 order by pr.Fecha desc, Declaracion_Estados.Id desc* 
 * 
 */
