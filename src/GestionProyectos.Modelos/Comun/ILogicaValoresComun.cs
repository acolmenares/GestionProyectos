using GestionProyectos.Modelos.Entidades;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Comun
{
    public interface ILogicaValoresComun
    {
        string ObtenerTipoIdentificacion(List<SubTablas> subtablas, Personas persona);

        string ObtenerDescripcionEstado(List<SubTablas> subtablas, DeclaracionEstados estado); 

        DeclaracionEstados ObtenerDeclaracionEstado(List<DeclaracionEstados> estados, Declaracion declaracion, int tipoEstadoId);
        PersonasContactos ObtenerContacto(List<PersonasContactos> contactos, Personas persona, int? idTipoContacto, bool concatenar = false);

        int ObtenerTFE(Declaracion declaracion);

        int? ObtenerTFR(List<Personas> personas, Declaracion declaracion);

        string ObtenerDescripcion(List<SubTablas> subtablas, int? subTablasId);

        string ObtenerGrupo(List<SubTablas> subtablas, Declaracion declaracion);
        string ObtenerFuente(List<SubTablas> subtablas, Declaracion declaracion);
        string ObtenerRegional(List<SubTablas> subtablas, Declaracion declaracion);
        string ObtenerMunicipioAtencion(List<SubTablas> subtablas, Declaracion declaracion);
        string ObtenerTipoDeclarante(List<SubTablas> subtablas, Declaracion declaracion);
        string ObtenerEnLinea(List<SubTablas> subtablas, Declaracion declaracion);

        string ObtenerAtendido(List<SubTablas> subtablas, Declaracion declaracion);


        string ObtenerCelular(List<PersonasContactos> contactos, Personas persona, bool concatenar = false);
        string ObtenerTelefono(List<PersonasContactos> contactos, Personas persona, bool concatenar = false);
        string ObtenerBarrio(List<PersonasContactos> contactos, Personas persona, bool concatenar = false);
        string ObtenerDireccion(List<PersonasContactos> contactos, Personas persona, bool concatenar = false);

        string ObtenerMotivoNoAtencion(List<SubTablas> subtablas, Declaracion declaracion);

        Personas PersonaDeclarante(List<Personas> personas, Declaracion declaracion);
        //string ProgramadoQueNoAsistio(List<SubTablas> subTablas, Declaracion declaracion);
    }
}