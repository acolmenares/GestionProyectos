using GestionProyectos.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Entidades
{
    [Alias("Declaracion_Bienes")]
    public class DeclaracionBienes : IConIdDeclaracion, IDeclaracionConIdSubTabla
    {
        public int Id { get; set; }
        [Alias("Id_Declaracion")]
        public int? Id_Declaracion { get; set; }
        [Alias("Id_Bienes")]
        public int? Id_SubTabla { get; set; }
        public string Tipo { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
    }
}

