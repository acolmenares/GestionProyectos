using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Interfaces;

namespace GestionProyectos.Modelos.Entidades
{
    [Alias("Declaracion_Personas_Ayuda")]
    public class DeclaracionPersonasAyuda: IConIdDeclaracion, IDeclaracionConIdSubTabla
    {
        public int Id { get; set; }
        [Alias("Id_Declaracion")]
        public int? Id_Declaracion { get; set; }
        [Alias("Id_Personas_Ayuda")]
        public int? Id_SubTabla { get; set; }
        [Alias("Id_Tipo_Entrega")]
        public int? TipoEntregaId { get; set; }
        [Alias("Id_Declaracion_Seguimiento")]
        public int? DeclaracionSeguimientoId { get; set; }
    }
}
