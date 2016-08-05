using GestionProyectos.Modelos.Interfaces;
using ServiceStack.Data;
using System;
using ServiceStack;
using ServiceStack.Caching;

namespace GestionProyectos.CAD
{
	public class FabricaBD : IFabricaRepositorios
    {
		public IDbConnectionFactory ConnectionFactory { get; set; }
		

        public FabricaBD()
        {
        }

        public IRepositorio Crear(bool crearTransaccion = false)
        {
			return new RepositorioBD(ConnectionFactory,  crearTransaccion);
        }

        public void Ejecutar(Action<IRepositorio> acciones, bool crearTransaccion = false)
        {
            using (var rp = Crear(crearTransaccion))
            {
                acciones(rp);
            }
        }

        public T Ejecutar<T>(Func<IRepositorio, T> acciones, bool crearTransaccion = false)// where T : IEntidad
        {
            var r = default(T);
            using (var rp = Crear(crearTransaccion))
            {
                r = acciones(rp);
            }
            return r;
        }
    }
}
