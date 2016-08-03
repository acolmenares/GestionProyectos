using ServiceStack;
using GestionProyectos.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Model;
using ServiceStack.Web;
using ServiceStack.Caching;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Declaraciones;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorBase
    {
        public IFabricaRepositorios Fabrica { get; set; }
        public ICacheClient Cache { get; set; }
        public IQueryResponseFactory QueryResponseFactory { get; set; }
        public IRepoHerramienta RepoHerramienta { get; set; }
        public ITransformoFechas TrasnformoFechas { get; set; }

        public IFabricaContructores FbContructores { get; set; }
        public IFabricaDatosObjetivos FbDatosObjectivos { get; set; }

        protected T Actualizar<T>(object peticion) 
            where T:IEntidad
        {                           
            var np= Convertir<T>(peticion);
            Fabrica.Ejecutar(rp => {
                rp.Actualizar<T>(np); 
            });
            return np;
        }

        protected int Borrar<T,TPeticion>(TPeticion peticion) 
            where TPeticion:IHasId<int>
            where T:IEntidad
        {
            var r = 0;
            Fabrica.Ejecutar(rp => {
                r= rp.Borrar<T>(peticion.Id);
            });
            return r;
        }


        protected List<T> Consultar<T>(object peticion)
            where T : IEntidad
        {
            var r = new List<T>();
            Fabrica.Ejecutar(rp =>
            {
                r = rp.Consultar<T>();
            });
            return r;
        }

        protected  QueryResponse<T> Consultar<T>(IQueryDb<T> modelo, IRequest peticion)
		{
            return QueryResponseFactory.Consultar<T>(modelo, peticion);
            /*
			QueryResponse<T> r=null;
			Fabrica.Ejecutar(rp => {
				r= rp.Consultar(modelo, peticion);
			});
			return r;*/
		}

        protected T Crear<T>(object peticion)
            where T : IEntidad
        {
            var dato = Convertir<T>(peticion);
            Fabrica.Ejecutar(rp => {
                rp.Crear(dato);
            });
            return dato;
        }


        private T Convertir<T>(object peticion)
            where T : IEntidad    
        {
            var np = typeof(T).CreateInstance<T>();
            np.PopulateWith(peticion);
            return np;
        }


		protected void Correr(Action<IRepositorio> conexion, bool crearTransaccion = false)
		{
			Fabrica.Ejecutar(conexion, crearTransaccion);
		}

		protected T Ejecutar<T>(Func<IRepositorio,T> conexion, bool crearTransaccion = false) where T:IEntidad
		{
			return Fabrica.Ejecutar<T>(conexion, crearTransaccion);
		}

        protected static T GetFromCache<T>(ICacheClient cache, string key, Func<T> tFunc, bool refrescarCache=false)
        {
            var r = default(T);
            if (!refrescarCache)
            {
                r = cache.Get<T>(key);
            }
            if (r != null) return r;

            r = tFunc();
            
            cache.Set(key, r, TimeSpan.FromMinutes(10));
            return r;

        }

        protected static List<SubTablas> GetSubTablas(ICacheClient cache, IRepositorio repo)
        {
            return GetFromCache(cache, "SubTablas", () => repo.Consultar<SubTablas>());
        }


        protected string GetKey<T>(T query) where T:ITengoFechaRadicacionDesdeHasta
        {
            return TrasnformoFechas.ConvertirEnLlave<T>(query); 
            
        }

        protected static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

    }
}
