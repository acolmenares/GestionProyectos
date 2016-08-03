using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class LogicaValoresComun : ILogicaValoresComun
    {
        //public int IdProgramadoQueNoAsistio;
        public LogicaValoresComun()
        {
            //IdProgramadoQueNoAsistio = 117;
        }

        public virtual PersonasContactos ObtenerContacto(List<PersonasContactos> contactos, Personas persona, int? idTipoContacto, bool concatenar = false)
        {
            return  contactos.FindAll(q => q.Id_Persona == persona.Id && q.Id_Tipo_Contacto == idTipoContacto)
                .OrderBy(q => q.Activo).OrderByDescending(q => q.Id).Take(1).SingleOrDefault()?? new PersonasContactos();            
        }

        public virtual string ObtenerDescripcion(List<SubTablas> subtablas, int? subTablasId)
        {
            var r = subtablas.FirstOrDefault(q => q.Id == subTablasId);
            return (r != default(SubTablas)) ? r.Descripcion : null;
        }

        public string ObtenerDescripcionEstado(List<SubTablas> subtablas, DeclaracionEstados estado)
        {
            return ObtenerDescripcion(subtablas, estado.Id_Como_Estado);
        }

        public string ObtenerAtendido(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_Atender);
        }

        public string ObtenerEnLinea(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_EnLinea);
        }

        public virtual DeclaracionEstados ObtenerDeclaracionEstado(List<DeclaracionEstados> estados, Declaracion declaracion, int tipoEstadoId)
        {
            return estados.FindAll(q => q.Id_Declaracion == declaracion.Id && q.Id_Tipo_Estado == tipoEstadoId)
                .OrderByDescending(q => q.Fecha).ThenByDescending(q => q.Id).Take(1).SingleOrDefault()??new DeclaracionEstados();
        }

        public string ObtenerTipoIdentificacion(List<SubTablas> subtablas, Personas persona)
        {
            return ObtenerDescripcion(subtablas, persona.Id_Tipo_Identificacion);
        }


        public string ObtenerFuente(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_Fuente);
        }

        public string ObtenerGrupo(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_Grupo);
        }

        public string ObtenerMunicipioAtencion(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_lugar_fuente);
        }

        public string ObtenerRegional(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_Regional);
        }

        public virtual int ObtenerTFE(Declaracion declaracion)
        {
            return (declaracion.Menores_Ninas.HasValue ? declaracion.Menores_Ninas.Value : 0) +
                (declaracion.Menores_Ninos.HasValue ? declaracion.Menores_Ninos.Value : 0) +
                (declaracion.Recien_Nacidos.HasValue ? declaracion.Recien_Nacidos.Value : 0) +
                (declaracion.Lactantes.HasValue ? declaracion.Lactantes.Value : 0) +
                (declaracion.Resto_Nucleo.HasValue ? declaracion.Resto_Nucleo.Value : 0);
        }

        public virtual int? ObtenerTFR(List<Personas> personas,  Declaracion declaracion)
        {
            if (!declaracion.Fecha_Valoracion.HasValue) return null;
            var pd = personas.FindAll(q => q.Id_Declaracion == declaracion.Id);
            return pd.Count();//personas.Count(f => f.Id_Declaracion == declaracion.Id);
        }

        public string ObtenerTipoDeclarante(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Tipo_Declaracion);
        }


        public Personas PersonaDeclarante(List<Personas> personas, Declaracion declaracion)
        {
            return personas.FirstOrDefault(q => q.Id_Declaracion == declaracion.Id && q.Tipo == "D");
        }


        public virtual string ObtenerDireccion(List<PersonasContactos> contactos, Personas persona, bool concatenar = false)
        {
            return ObtenerContacto(contactos, persona, 74, concatenar).Descripcion;
        }

        public virtual string ObtenerTelefono(List<PersonasContactos> contactos, Personas persona, bool concatenar = false)
        {
            return ObtenerContacto(contactos, persona, 75, concatenar).Descripcion;
        }

        public virtual string ObtenerCelular(List<PersonasContactos> contactos, Personas persona, bool concatenar = false)
        {
            return ObtenerContacto(contactos, persona, 76, concatenar).Descripcion;
        }

        public virtual string ObtenerBarrio(List<PersonasContactos> contactos, Personas persona, bool concatenar = false)
        {
            return ObtenerContacto(contactos, persona, 79, concatenar).Descripcion;
        }

        public virtual string ObtenerMotivoNoAtencion(List<SubTablas> subtablas, Declaracion declaracion)
        {
            return ObtenerDescripcion(subtablas, declaracion.Id_Motivo_Noatender);
        }

        /*
        public string ProgramadoQueNoAsistio (List<SubTablas> subtablas, Declaracion declaracion)
        {
            return declaracion.Id_Motivo_Noatender.HasValue && declaracion.Id_Motivo_Noatender.Value == IdProgramadoQueNoAsistio?"Si":null;
        }
        */

    }
}
