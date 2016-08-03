using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Modelos.Interfaces
{
    public interface IFabricaRepositorios
    {
        IRepositorio Crear(bool crearTransaccion = false);
        void Ejecutar(Action<IRepositorio> conexion, bool crearTransaccion = false);
        T Ejecutar<T>(Func<IRepositorio, T> conexion, bool crearTransaccion = false) where T : IEntidad;
        //void Ejecutar(Action<IRepositorio> p);
    }
}
