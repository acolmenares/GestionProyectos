using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GestionProyectos.Modelos.Interfaces
{
    public partial interface IRepositorio :IDisposable
	{        
        List<PersonasPorDeclaracion> ContarPersonasPorDeclaracion(IEnumerable<int> idsDeclaraciones);

        List<Declaracion> ConsultarDeclaracion<T>(QueryDataDeclaracion<T> modelo);

        List<Personas> ConsultarPersonasDeclaracion(IEnumerable<int> idsDeclaraciones, string tipoPersona=null);

        List<PersonasContactos> ConsultarPersonasContactos(IEnumerable<int> idsDeclaraciones, Expression<Func<PersonasContactos, object>> fields = null);
        List<DeclaracionEstados> ConsultarDeclaracionEstados(List<int> idsDeclaraciones, Expression<Func<DeclaracionEstados, object>> fields = null);
    }
}

