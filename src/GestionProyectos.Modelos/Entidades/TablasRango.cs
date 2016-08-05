using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Entidades
{
    public class TablasRango
    {
        public TablasRango()
        {
            
            Declaracion = new List<Declaracion>();
            Personas = new List<Personas>();
            Programacion = new List<Programacion>();
            SubTablas = new List<SubTablas>();
            DeclaracionEstados = new List<DeclaracionEstados>();
            PersonasContactos = new List<PersonasContactos>();

        }

        public List<Declaracion> Declaracion { get; set; }
        public List<Personas> Personas { get; set; }
        public List<Programacion> Programacion { get; set; }
        public List<SubTablas> SubTablas { get; set; }
        public List<DeclaracionEstados> DeclaracionEstados { get; set; }
        public List<PersonasContactos> PersonasContactos { get; set; }

    }
}
