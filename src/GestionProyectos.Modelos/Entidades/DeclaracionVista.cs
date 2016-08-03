using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Entidades
{
    public class DeclaracionVista
    {
        public int Id { get; set; }
        public DateTime FechaRadicacion { get; set; }
        public DateTime FechaDesplazamiento { get; set; }
        public DateTime FechaDeclaracion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public string Horario { get; set; }
        public string Grupo { get; set; }
        public string Fuente { get; set; }
        public string Regional { get; set; }
        public string MunicipioAtencion { get; set; }
        public string TipoDeclarante { get; set; }
        public string EnLinea { get; set; }
        public string TipoPersona { get; set; }
        public string Atendido { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string TI { get; set; }
        public string Identificacion { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Etnia { get; set; }
        public int? Gestantes { get; set; }
        public int? Menores { get; set; }
        public int? RecienNacidos { get; set; }
        public int? Lactantes { get; set; }
        public int? RestoNucleo { get; set; }
        public int? TFE { get; set; }
        public int? TFR { get; set; }
        public string MotivoNoAtencion { get; set; }
        public string TipoTenencia { get; set; }
        public string Habitaciones { get; set; }
        public string PersonasVivienda { get; set; }
        public string PersonasHabitacion { get; set; }
        public string MaterialesVivienda { get; set; }
        public string AguaPotable { get; set; }
        public string ObtencionAgua { get; set; }
        public string TipoDesplazamiento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string ConcejoResguardo { get; set; }
        public string Vereda { get; set; }
        public string CuantasVecesDesplazado { get; set; }
        public string HaDeclaradoAntes { get; set; }
        public DateTime? FechaDesplazamientoAnterior { get; set; }
        public string LugarDesplazamientoAntes { get; set; }
        public string HaRegresado { get; set; }
        public string CausasDesplazamiento { get; set; }
        public string TiempoEnDeclarar { get; set; }
        public string PorqueTardoEnDeclarar { get; set; }
        public string EsDelMunicipio { get; set; }
        public string PorqueNoDeclaroEnMunicipio { get; set; }
        public string ADDFamiliasAccion { get; set; }
        public string MunicipioFamiliasAccion { get; set; }
        public string ADDUnidos { get; set; }
        public string MunicipioUnidos { get; set; }
        public string SolicitoAyuda { get; set; }
        public string EntidadInicial { get; set; }
        public string ComoFueAtencion { get; set; }
        public string FuenteIngresos { get; set; }
        public decimal? PromedioIngresos { get; set; }
        public string DaniosFamila { get; set; }
        public string VBGGeneral { get; set; }
        public string VBGGeneralAgresor { get; set; }
        public string ADDMuerto { get; set; }
        public string DDDMuerto { get; set; }
        public string Desaparecido { get; set; }
        public string Retornaria { get; set; }
        public string MotivoNoRetornaria { get; set; }
        public string AtencionPsicosocial { get; set; }
        public string HaAfectadoDesplazamiento { get; set; }
        public string AyudaHablar { get; set; }
        public string PersonasAyudaHablar { get; set; }
        public string Adiccion { get; set; }
        public string AdiccionAlcohol { get; set; }
        public string AdiccionDroga { get; set; }
        public string BienesPerdio { get; set; }
        public string TipoBienRural { get; set; }
        public string DocumentoPropiedad { get; set; }
        public string CopiaDocumento { get; set; }
        public string DestinoTierra { get; set; }
        public string SituacionActual { get; set; }
        public string ApoyoEmocional { get; set; }
    }
        
}
