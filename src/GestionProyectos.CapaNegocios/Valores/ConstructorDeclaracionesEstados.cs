using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using System.Collections.Generic;
using System.Linq;

namespace GestionProyectos.CapaNegocios.Valores
{
    public class ConstructorDeclaracionesEstados : IConstructorDeclaracionesEstados
    {
        public ILogicaValoresEstados LogicaValoresEstados  { get; set; }

        public TablasRango Tablas { get; set; }
                
        public ConstructorDeclaracionesEstados()
        {
            Tablas = new TablasRango();
        }

        public List<DeclaracionesEstados> Data
        {
            get
            {                
                var r = new List<DeclaracionesEstados>();
                Tablas.Declaracion.ForEach(declaracion => {
                    var persona = LogicaValoresEstados.PersonaDeclarante(Tablas.Personas, declaracion);
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
            r.Grupo = fv.ObtenerGrupo(Tablas.SubTablas, declaracion);
            r.Fuente = fv.ObtenerFuente(Tablas.SubTablas, declaracion);
            r.Regional = fv.ObtenerRegional(Tablas.SubTablas, declaracion);
            r.MunicipioAtencion = fv.ObtenerMunicipioAtencion(Tablas.SubTablas, declaracion);
            r.TipoDeclarante = fv.ObtenerTipoDeclarante(Tablas.SubTablas, declaracion);
            r.EnLinea = fv.ObtenerEnLinea(Tablas.SubTablas, declaracion);

            r.TFE = fv.ObtenerTFE(declaracion);
            r.TFR = fv.ObtenerTFR(Tablas.Personas, declaracion);

            r.TI = fv.ObtenerTipoIdentificacion(Tablas.SubTablas, persona);

            r.Celular = fv.ObtenerCelular(Tablas.PersonasContactos, persona);
            r.Telefono = fv.ObtenerTelefono(Tablas.PersonasContactos, persona);
            r.Barrio = fv.ObtenerBarrio(Tablas.PersonasContactos, persona);
            r.Direccion = fv.ObtenerDireccion(Tablas.PersonasContactos, persona);

            var estado = fv.ObtenerEstadoElegibilidad(Tablas.DeclaracionEstados, declaracion);
            r.Elegibilidad = fv.EsElegible(Tablas.DeclaracionEstados, Tablas.SubTablas, declaracion);
            r.FechaElegibilidad = estado.Fecha;

            estado = fv.ObtenerEstadoContactado(Tablas.DeclaracionEstados, declaracion);
            r.Contactado = fv.ObtenerDescripcionEstado(Tablas.SubTablas, estado);
            r.FechaContactado = estado.Fecha;

            estado = fv.ObtenerEstadoProgramado(Tablas.SubTablas, Tablas.Programacion, Tablas.DeclaracionEstados, declaracion);
            r.Programado = fv.ObtenerDescripcionEstado(Tablas.SubTablas, estado);
            r.FechaProgramado = estado.Fecha;

            estado = fv.ObtenerEstadoReprogramado(Tablas.DeclaracionEstados, declaracion);
            r.Reprogramado = fv.ObtenerDescripcionEstado(Tablas.SubTablas, estado);
            r.FechaReprogramado = estado.Fecha;

            r.Atendido = fv.ObtenerAtendido(Tablas.SubTablas, declaracion);
            r.MotivoNoAtencion = fv.ObtenerMotivoNoAtencion(Tablas.SubTablas, declaracion);
            r.MotivoNoAtencionId = declaracion.Id_Motivo_Noatender;

            r.TipoReprogramacion = fv.ObtenerTipoReprogramacion(Tablas.SubTablas, Tablas.Programacion, Tablas.DeclaracionEstados, declaracion);

            estado= fv.ObtenerEstadoSegundaEntrega(Tablas.SubTablas, Tablas.Programacion, Tablas.DeclaracionEstados, declaracion);
            r.FechaSegundaEntrega = estado.Fecha;
            r.AsistioSegundaEntrega = fv.ObtenerDescripcion(Tablas.SubTablas, estado.Id_Asistio);

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
