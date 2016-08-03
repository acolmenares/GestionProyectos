using GestionProyectos.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Entidades
{
    public class Regional:IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
