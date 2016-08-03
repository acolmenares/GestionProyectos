using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class LogicaValoresEstados : LogicaValoresComun, ILogicaValoresEstados, ILogicaValoresComun
    {   

        public virtual DeclaracionEstados ObtenerEstadoElegibilidad(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, 4036);
        }

        public virtual DeclaracionEstados ObtenerEstadoContactado(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, 4037);
        }

        public virtual DeclaracionEstados ObtenerEstadoProgramado(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, 4038);
        }

        public virtual DeclaracionEstados ObtenerEstadoReprogramado(List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            return ObtenerDeclaracionEstado(estados, declaracion, 4039);
        }

        
        public virtual string ObtenerTipoReprogramacion(List<SubTablas> subtablas, List<Programacion> programacion, List<DeclaracionEstados> estados, Declaracion declaracion)
        {
            
            var estado=  estados.FindAll(
                q => q.Id_Declaracion == declaracion.Id 
                && (q.Id_Tipo_Estado == 4038 || q.Id_Tipo_Estado == 4039) 
                && q.Id_Asistio == 20)
                .OrderByDescending(q => q.Fecha).ThenByDescending(q => q.Id).Take(1).SingleOrDefault() ?? new DeclaracionEstados();

            var prg = programacion.SingleOrDefault(q => q.Id == estado.Id_Programa) ?? new Programacion();
            
            return ObtenerDescripcion(subtablas, prg.Id_TipoEntrega);
        }

        public DeclaracionEstados ObtenerEstadoSegundaEntrega(List<SubTablas> subTablas, List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion)
        {
            return 
            declaracionEstados.Where(q => q.Id_Declaracion == declaracion.Id
                                      && (q.Id_Tipo_Estado == 4038 || q.Id_Tipo_Estado == 4039))
                                      .Join(programacion, a => a.Id_Programa, b => b.Id, (a, b) => new DeclaracionEstados
                                      {
                                          Id= a.Id,
                                          Fecha= b.Fecha,
                                          Id_Asistio= a.Id_Asistio,
                                          Id_Declaracion= a.Id_Declaracion
                                          
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
