using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using System.Collections.Generic;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class ConstructorDeclaracionesEstados : IConstructorDeclaracionesEstados
    {
        public ILogicaValoresEstados LogicaValoresEstados  { get; set; }

        public List<Declaracion> Declaraciones { get; set; }

        public List<Personas> Personas { get; set; }
        public List<SubTablas> SubTablas { get; set; }
        public List<PersonasContactos> PersonasContactos { get; set; }
        public List<DeclaracionEstados> DeclaracionEstados { get; set; }
        public List<Programacion> Programacion { get; set; }
        
        public ConstructorDeclaracionesEstados()
        {
            Declaraciones = new List<Declaracion>();
            Personas = new List<Personas>();
            SubTablas = new List<SubTablas>();
            PersonasContactos = new List<PersonasContactos>();
            Programacion = new List<Programacion>();
        }

        public List<DeclaracionesEstados> Data
        {
            get
            {
                
                var r = new List<DeclaracionesEstados>();
                Declaraciones.ForEach(declaracion => {
                    var persona = LogicaValoresEstados.PersonaDeclarante(Personas, declaracion);
                    if (persona != default(Personas))
                    {
                        DeclaracionesEstados declaracionEstado = Convertir(LogicaValoresEstados, declaracion, persona);
                        r.Add(declaracionEstado);
                    }
                });
                return r;
            }
        }

        private  DeclaracionesEstados Convertir(ILogicaValoresEstados fv, Declaracion declaracion, Personas persona)
        {
            var r = new DeclaracionesEstados();
            PopularCon(r, declaracion);
            PopularCon(r, persona);
            PopularCon(r, declaracion, persona, fv) ;

            return r;
        }

        private void PopularCon(DeclaracionesEstados r, Declaracion declaracion, Personas persona, ILogicaValoresEstados fv )
        {
            r.Grupo = fv.ObtenerGrupo(SubTablas, declaracion);
            r.Fuente = fv.ObtenerFuente(SubTablas, declaracion);
            r.Regional = fv.ObtenerRegional(SubTablas, declaracion);
            r.MunicipioAtencion = fv.ObtenerMunicipioAtencion(SubTablas, declaracion);
            r.TipoDeclarante = fv.ObtenerTipoDeclarante(SubTablas, declaracion);
            r.EnLinea = fv.ObtenerEnLinea(SubTablas, declaracion);

            r.TFE = fv.ObtenerTFE(declaracion);
            r.TFR = fv.ObtenerTFR(Personas, declaracion);

            r.TI = fv.ObtenerTipoIdentificacion(SubTablas, persona);

            r.Celular = fv.ObtenerCelular(PersonasContactos, persona);
            r.Telefono = fv.ObtenerTelefono(PersonasContactos, persona);
            r.Barrio = fv.ObtenerBarrio(PersonasContactos, persona);
            r.Direccion = fv.ObtenerDireccion(PersonasContactos, persona);

            var estado = fv.ObtenerEstadoElegibilidad(DeclaracionEstados, declaracion);
            r.Elegibilidad = fv.ObtenerDescripcionEstado(SubTablas, estado);
            r.FechaElegibilidad = estado.Fecha;

            estado = fv.ObtenerEstadoContactado(DeclaracionEstados, declaracion);
            r.Contactado = fv.ObtenerDescripcionEstado(SubTablas, estado);
            r.FechaContactado = estado.Fecha;

            estado = fv.ObtenerEstadoProgramado(DeclaracionEstados, declaracion);
            r.Programado = fv.ObtenerDescripcionEstado(SubTablas, estado);
            r.FechaProgramado = estado.Fecha;

            estado = fv.ObtenerEstadoReprogramado(DeclaracionEstados, declaracion);
            r.Reprogramado = fv.ObtenerDescripcionEstado(SubTablas, estado);
            r.FechaReprogramado = estado.Fecha;

            r.Atendido = fv.ObtenerAtendido(SubTablas, declaracion);
            r.MotivoNoAtencion = fv.ObtenerMotivoNoAtencion(SubTablas, declaracion);
            r.MotivoNoAtencionId = declaracion.Id_Motivo_Noatender;

            r.TipoReprogramacion = fv.ObtenerTipoReprogramacion(SubTablas, Programacion, DeclaracionEstados, declaracion);

            estado= fv.ObtenerEstadoSegundaEntrega(SubTablas, Programacion, DeclaracionEstados, declaracion);
            r.FechaSegundaEntrega = estado.Fecha;
            r.AsistioSegundaEntrega = fv.ObtenerDescripcion(SubTablas, estado.Id_Asistio);

            //r.ProgramadoQueNoAsistio= fv.ProgramadoQueNoAsistio(SubTablas, declaracion);
        }

        private static void PopularCon(DeclaracionesEstados r, Declaracion declaracion)
        {
            r.Id = declaracion.Id;
            r.FechaAtencion = declaracion.Fecha_Valoracion;
            r.FechaDeclaracion = declaracion.Fecha_Declaracion;
            r.FechaDesplazamiento = declaracion.Fecha_Desplazamiento;
            r.FechaRadicacion = declaracion.Fecha_Radicacion;
            r.Horario = declaracion.Horario;
            r.FechaSegundaEntrega = declaracion.Fecha_Segunda_Encuesta;
        }

        private static void PopularCon(DeclaracionesEstados r, Personas persona)
        {
            r.Identificacion = persona.Identificacion;
            r.PrimerApellido = persona.Primer_Apellido;
            r.SegundoApellido = persona.Segundo_Apellido;
            r.PrimerNombre = persona.Primer_Nombre;
            r.SegundoNombre = persona.Segundo_Nombre;
        }

    }
}
