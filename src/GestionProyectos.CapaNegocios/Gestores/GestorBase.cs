using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Model;
using ServiceStack.Web;
using System;
using System.Collections.Generic;

namespace GestionProyectos.CapaNegocios.Gestores
{
    public class GestorBase
    {
        public IFabricaRepositorios Fabrica { get; set; }
        public ICacheClient Cache { get; set; }
        public IQueryResponseFactory QueryResponseFactory { get; set; }
        public IRepoHerramienta RepoHerramienta { get; set; }
        public ITransformoFechas TransformoFechas { get; set; }

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

        protected T GetFromCache<T>(ITengoFechaRadicacionDesdeHasta query, Func<T> tFunc, bool refrescarCache=false)
        {
            var key = GetKey<T>(query);
            var r = default(T);
            if (!refrescarCache)
            {
                r = Cache.Get<T>(key);
            }
            if (r != null) return r;

            r = tFunc();
            
            Cache.Set(key, r, TimeSpan.FromMinutes(10));
            return r;

        }

        //protected  List<SubTablas> GetSubTablas(ICacheClient cache, IRepositorio repo)
        //{
        //    return GetFromCache( "SubTablas", () => repo.Consultar<SubTablas>());
        //}


        protected string GetKey<T>(ITengoFechaRadicacionDesdeHasta query) 
        {
            return TransformoFechas.ConvertirEnLlave<T>(query); 
        }

        protected string GetRango(ITengoFechaRadicacionDesdeHasta query) 
        {
            return TransformoFechas.ConvertirEnRango(query);
        }


        protected  string FormatDate(DateTime dateTime)
        {
            return TransformoFechas.EnAAAAMMDD(dateTime); // I dateTime.ToString("yyyyMMdd");
        }

        protected TablasRango CargarTablasRango(IRepositorio rp, ITengoFechaRadicacionDesdeHasta modelo, bool refrescarCache=false)
        {
            var rango = GetRango(modelo);

            var subtablas = GetFromCache(modelo, () => RepoHerramienta.Consultar(rango, () => rp.Consultar<SubTablas>()), refrescarCache);

            var declaraciones = GetFromCache(modelo, () => RepoHerramienta.Consultar(rango, () => rp.ConsultarDeclaracion(modelo)), refrescarCache);
            var idsDeclaraciones = declaraciones.ConvertAll(q => q.Id);

            var personas = GetFromCache(modelo,
                () => RepoHerramienta.Consultar(rango, () => rp.ConsultarPersonasDeclaracion(idsDeclaraciones)),
                refrescarCache);  //  new List<Personas>();  //
            var idsPersonas = personas.ConvertAll(q => q.Id);
            var contactos = GetFromCache(modelo, 
                () => RepoHerramienta.Consultar(rango, () => rp.ConsultarPersonasContactos(idsPersonas)),
                refrescarCache); //, (q) => new { q.Id_Persona, q.Id_Tipo_Contacto, q.Activo, q.Descripcion});
            var dEstados = GetFromCache(modelo, 
                () => RepoHerramienta.Consultar(rango, () => rp.ConsultarDeclaracionEstados(idsDeclaraciones)),
                refrescarCache);
            var programacion = GetFromCache(modelo, () => RepoHerramienta.Consultar(rango, () => rp.Consultar<Programacion>()), refrescarCache);

            return new TablasRango
            {
                Declaracion = declaraciones,
                DeclaracionEstados = dEstados,
                Personas = personas,
                PersonasContactos = contactos,
                Programacion = programacion,
                SubTablas = subtablas,
            };
        }

    }
}
