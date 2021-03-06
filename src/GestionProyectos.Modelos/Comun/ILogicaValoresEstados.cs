﻿using GestionProyectos.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Comun
{
    public interface ILogicaValoresEstados:ILogicaValoresComun

    {
        string EsElegible(List<DeclaracionEstados> estados, List<SubTablas> subtablas, Declaracion declaracion);
        DeclaracionEstados ObtenerEstadoElegibilidad(List<DeclaracionEstados> estados, Declaracion declaracion);
        DeclaracionEstados ObtenerEstadoContactado(List<DeclaracionEstados> estados, Declaracion declaracion);
        DeclaracionEstados ObtenerEstadoProgramado(List<SubTablas> subTablas, List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion);
        DeclaracionEstados ObtenerEstadoReprogramado(List<DeclaracionEstados> estados, Declaracion declaracion);
        string ObtenerTipoReprogramacion(List<SubTablas> subtablas, List<Programacion> programacion, List<DeclaracionEstados> estados, Declaracion declaracion);
        DeclaracionEstados ObtenerEstadoSegundaEntrega(List<SubTablas> subTablas, List<Programacion> programacion, List<DeclaracionEstados> declaracionEstados, Declaracion declaracion);
    }
}
