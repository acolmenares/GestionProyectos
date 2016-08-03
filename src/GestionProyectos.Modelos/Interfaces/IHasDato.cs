using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IHasDato<T>
    {
        T Dato { get; set; }
    }
}
