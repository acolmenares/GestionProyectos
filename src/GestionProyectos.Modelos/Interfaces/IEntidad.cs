using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    /// <summary>
    /// Todas las Entidades de nuestra BD deben tener un Id tipo int
    /// </summary>
    public interface IEntidad:IHasId<int>
    {
        new int Id { get; set; }
    }
}
