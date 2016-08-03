using Funq;
using GestionProyectos.CAD;
using GestionProyectos.CapaNegocios.Gestores;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using GestionProyectos.Modelos.Peticiones.Proyectos;
using GestionProyectos.Servicios;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Data;
using ServiceStack.Auth;
using ServiceStack.Admin;
using GestionProyectos.CapaNegocios.Valores;
using GestionProyectos.Modelos.Comun;

namespace GestionProyectos.WebHost
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base("Gestión de Contraseñas", typeof(ServicioBase).Assembly) { }

		public override void Configure(Container container)
		{
			SetConfig(new HostConfig
			{
				DebugMode = true,
				HandlerFactoryPath = "gp-api",
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				DefaultContentType = "json"
			});

            


			Plugins.Add(new CorsFeature());
			Plugins.Add(new SessionFeature()); // TODO : PONER REDIS AQUI!
			Plugins.Add(new AdminFeature());

			Plugins.Add(new AutoQueryDataFeature { MaxLimit = 20 });

			Plugins.Add(new AuthFeature(() => new AuthUserSession(),
										new IAuthProvider[] { new BasicAuthProvider()  },
			                            "~/metadata"
									   ));


			Routes.Add<ConsultarProyectos>("/consultar/proyectos", ApplyTo.Get);
			Routes.Add<EncontrarProyecto>("/encontrar/proyecto", ApplyTo.Get);
			Routes.Add<CrearProyecto>("/crear/proyecto", ApplyTo.Post);
			Routes.Add<BorrarProyecto>("/borrar/proyecto/{Id}", ApplyTo.Post);
			Routes.Add<ActualizarProyecto>("/actualizar/proyecto", ApplyTo.Post);

			var appSettings = new AppSettings();

            var dbfactory = SQLConnectionFactory(appSettings); 
            //var dbfactory = SqliteConnectionFactory(); 
            //MySqlConnectionFactory(appSettings);
			CrearTablas(dbfactory);

            container.Register<IDbConnectionFactory>(dbfactory);
            container.RegisterAs<FabricaBD, IFabricaRepositorios>();

            container.RegisterAs<QueryResponseFactory, IQueryResponseFactory>().ReusedWithin(ReuseScope.None); // el mismo scope de AutoDataQuery

            container.RegisterAs<FabricaConstructores, IFabricaContructores>();
            container.RegisterAs<FabricaDatosObjetivos, IFabricaDatosObjetivos>();

            container.RegisterAs<TransformoFechas, ITransformoFechas>();
            container.RegisterAs<LogicaValoresEstados, ILogicaValoresEstados>();
            container.RegisterAs<ReglasObjetivoDosUno, IReglasObjetivoDosUno>();

            container.RegisterAs<GestorProyectos,IGestorProyectos>().ReusedWithin(ReuseScope.Request);
			container.RegisterAs<GestorRegionales,IGestorRegionales>().ReusedWithin(ReuseScope.Request);
			container.RegisterAs<GestorPersonas, IGestorPersonas>().ReusedWithin(ReuseScope.Request);


            container.RegisterAs<RepoHerramientaEnArchivos, IRepoHerramienta>();

            container.RegisterAs<GestorDeclaracionesEstados, IGestorDeclaracionesEstados>(); //.ReusedWithin(ReuseScope.Request);
            container.RegisterAs<GestorObjetivoDosUno, IGestorObjetivoDosUno>();

            container.Register<ICacheClient>(new MemoryCacheClient { FlushOnDispose = false });

            //var z = "~/".MapProjectPath();
            //Console.WriteLine(z);

           // Console.WriteLine(base.VirtualFiles.RootDirectory);

            //Console.WriteLine(base.VirtualFileSources.RootDirectory);

            //typeof(ConsultarProyectos).AddAttributes(new AuthenticateAttribute());
            //typeof(EncontrarProyecto).AddAttributes(new AuthenticateAttribute());
            //

        }

        static OrmLiteConnectionFactory SQLConnectionFactory(AppSettings appSettings)
		{
			var conexionBDSeguridad = appSettings.Get("ConexionGestionProyectos", Environment.GetEnvironmentVariable("APP_CONEXION_GP"));

			var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			return dbfactory;
		}

		static OrmLiteConnectionFactory MySqlConnectionFactory(AppSettings appSettings)
		{
			var conexionBDSeguridad = appSettings.Get("ConexionGestionProyectos", Environment.GetEnvironmentVariable("APP_CONEXION_GP"));

			var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, MySqlDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			return dbfactory;
		}


		static OrmLiteConnectionFactory SqliteConnectionFactory()
		{
			var conexionBDSeguridad = "~/App_Data/gp.sqlite".MapAbsolutePath();

			var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqliteDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			return dbfactory;
		}

		static void CrearTablas(OrmLiteConnectionFactory factory)
		{
			using (var cn = factory.OpenDbConnection())
			{
				cn.CreateTableIfNotExists<Proyecto>();
				cn.CreateTableIfNotExists<Regional>();
				cn.CreateTableIfNotExists<Persona>();
			}
		}

	}
}