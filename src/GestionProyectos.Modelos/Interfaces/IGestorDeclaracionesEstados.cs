using GestionProyectos.Modelos.Entidades;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Peticiones.Declaraciones;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IGestorDeclaracionesEstados
    {        
        QueryResponse<DeclaracionesEstados> Consultar(QueryDeclaracionesEstados query, IRequest request);
        void Crear(CrearDeclaracionesEstados query, IRequest request);
    }
}
