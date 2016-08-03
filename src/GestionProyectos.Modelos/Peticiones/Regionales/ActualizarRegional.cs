using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Peticiones.Regionales
{
    public class ActualizarRegional
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }

    public class ActualizarRegionalResponse : ResponseBase<Regional>, IHasDato<Regional>, IHasResponseStatus
    {

    }

}
