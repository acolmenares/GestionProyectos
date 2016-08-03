using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Servicios
{
    public class ServicioObjetivoDosUno:Service
    {
        public IGestorObjetivoDosUno Gestor { get; set; }

        public QueryResponse<ObejtivoDosUno> Get(QueryObjetivoDosUno query)
        {

            return Gestor.Consultar(query, Request);
        }
    }
}
