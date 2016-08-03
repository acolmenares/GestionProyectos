using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Interfaces;

namespace GestionProyectos.Modelos.Entidades
{
    
    [Alias("Declaracion_Causas_Desplazamiento")]
    public class DeclaracionCausasDesplazamiento:IConIdDeclaracion, IDeclaracionConIdSubTabla
    {
        public int Id { get; set; }
        [Alias("Id_Declaracion")]
        public int? Id_Declaracion { get; set; }
        [Alias("Id_Causa_Desplazamiento")]
        public int? Id_SubTabla { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
    }

}
