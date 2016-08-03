using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Entidades
{
    [Alias("Personas_Contactos")]
    public class PersonasContactos
    {
        public int Id { get; set; }
        public int? Id_Persona { get; set; }
        public int? Id_Tipo_Contacto { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Creacion { get; set; }
        public int? Id_Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? Id_Usuario_Modificacion { get; set; }
        public DateTime? Fecha_Cierre { get; set; }
        public bool? Cierre { get; set; }
    }

}
