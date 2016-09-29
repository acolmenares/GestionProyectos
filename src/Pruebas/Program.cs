using GestionProyectos.CAD;
using GestionProyectos.CapaNegocios.Valores;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("aqui vamos");

            var qr = new QueryResponse<DeclaracionesEstados>();
            var type = qr.GetType();
            Console.WriteLine(type.Name);


            PruebaLeerArchivoDeclaracionesEstadosJson();
            //ProbarHerramientaEnArchivos();


            Console.WriteLine("fin");
            Console.ReadLine();
        }

        private static void ProbarHerramientaEnArchivos()
        {

            var tf = new TransformoFechas();
            var rh = new RepoHerramientaEnArchivos { TransformoFechas = tf };

            var crearRango = new CrearRango() {
                Fecha_RadicacionGreaterThanOrEqualTo = new DateTime(2015, 10, 01),
                Fecha_RadicacionLessThanOrEqualTo = new DateTime(2016, 08, 31),
            };

            var rango = tf.ConvertirEnRango(crearRango);
            var tablasRango = new TablasRango();

            rh.CrearRango(crearRango, ()=>tablasRango);

            var rangos = rh.ConsultarRango();

            rangos.ForEach( r=>
            {
                Console.WriteLine(r.Nombre);
            });


            var appSettings = new AppSettings();
            var connectionFactory = SQLConnectionFactory(appSettings);
            var factoryDb = new FabricaBD { ConnectionFactory = connectionFactory };

            factoryDb.Ejecutar(rp => {
                tablasRango.Declaracion = rp.ConsultarDeclaracion(crearRango);
                var idsDeclaraciones = tablasRango.Declaracion.ConvertAll(q => q.Id);
                tablasRango.DeclaracionEstados = rp.ConsultarDeclaracionEstados(idsDeclaraciones);
                tablasRango.Personas = rp.ConsultarPersonasDeclaracion(idsDeclaraciones);
                tablasRango.Programacion = rp.Consultar<Programacion>();
                tablasRango.SubTablas = rp.Consultar<SubTablas>();
                tablasRango.PersonasContactos = rp.ConsultarPersonasContactos(idsDeclaraciones);
            });

            rh.CrearRango(crearRango, () => factoryDb.Ejecutar(rp =>
            {
                tablasRango.Declaracion = rp.ConsultarDeclaracion(crearRango);
                var idsDeclaraciones = tablasRango.Declaracion.ConvertAll(q => q.Id);
                tablasRango.DeclaracionEstados = rp.ConsultarDeclaracionEstados(idsDeclaraciones);
                tablasRango.Personas = rp.ConsultarPersonasDeclaracion(idsDeclaraciones);
                tablasRango.Programacion = rp.Consultar<Programacion>();
                tablasRango.SubTablas = rp.Consultar<SubTablas>();
                tablasRango.PersonasContactos = rp.ConsultarPersonasContactos(idsDeclaraciones);
                return tablasRango;
            }));

            rangos = rh.ConsultarRango();

            rangos.ForEach(r =>
            {
                Console.WriteLine(r.Nombre);
            });

            var d = rh.Consultar(rango, () => new List<Declaracion>());
            Console.WriteLine(d.Count);

            var qde = new QueryDeclaracionesEstados();
                       

            var lv = new LogicaValoresEstados();
            
            var fc = new FabricaConstructores();

            var de = fc.ContructorDeclaracionesEstados(lv,tablasRango).Data;

            var fr = new QueryResponse<DeclaracionesEstados> { Results = de, Total = de.Count };
            rh.Crear(rango, ()=> fr);

            var rr = rh.Consultar(rango, () => new QueryResponse<DeclaracionesEstados>());

            Console.WriteLine("{0} == {1}", fr.Results.Count, rr.Results.Count);
                
            //rh.QueryDeclaracionesEstados(rango, () => new QueryResponse<DeclaracionesEstados>());
        }

        private static void PruebaLeerArchivoDeclaracionesEstadosJson()
        {
            var de = ReadFromFile<QueryResponse<DeclaracionesEstados>>("QueryDeclaracionesEstados.json").Results
             .Where(q => q.MunicipioAtencion == "Florencia" && q.FechaRadicacion >= new DateTime(2016, 8, 1)
             && !q.FechaAtencion.HasValue && q.Elegibilidad=="Si"
             ).ToList(); // && q.Id >= 49368).ToList();

            var tf = new FabricaTransformoFechas().TransformoFechas();
            var datosObjetivo = new FabricaDatosObjetivos().DatosObjetivoDosUno(de, tf);
            var reglas = new FabricaReglas().ReglasObjetivoDosUno();
            var r = new FabricaConstructores().ConstructorObjetivoDosUno(datosObjetivo, reglas, tf).Data;
            r.ForEach(dato =>
            {
                Console.WriteLine("{0} {1} {2} {3} d:{4} P:{5}  Q:{6}  R{7} S:{8} ",
                    dato.Periodo, PadLeft(dato.Regional), PadLeft(dato.Municipio), PadLeft(dato.Mes),
                    PadLeft(dato.Radicados),
                    PadLeft(dato.Excluidos),
                    PadLeft(dato.Elegibles),
                    PadLeft(dato.AtendidosPrimeraEntregaEnElMesDeRadicacion),
                    PadLeft(dato.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores));

                if (dato.Municipio == "Florencia" && dato.Mes == "Agosto")
                {
                    dato.MotivosExclusion.ForEach(m =>
                    {
                        Console.WriteLine("{0} {1} {2} ", m.Id, m.Motivo, m.Cantidad);
                    });
                }
            });

            r.ForEach(dato =>
            {
                Console.WriteLine("{0} {1} {2}  --  {3} {4} ",
                    PadLeft(dato.AtendidosPrimeraEntregaEnElMesDeRadicacion),
                    PadLeft(dato.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores),
                    PadLeft(dato.AtendidosPrimeraEntregaRadicadosEnPeriodosAnteriores),
                    PadLeft(dato.AtendidosPrimeraEntregaEnMesesPosteriores),
                    PadLeft(dato.AtendidosPrimeraEntregaEnPeriodosPosteriores));
            });

            r.ForEach(dato =>
            {
                Console.WriteLine("{0} {1} {2}    {3} {4}  {5} ",
                    PadLeft(dato.PendientePorAplicarFiltros),
                    PadLeft(dato.PendientePorProgramar),
                    PadLeft(dato.PendienteNoContactado),
                    PadLeft(dato.PendienteProgramadoProximoMes),
                    PadLeft(dato.PendienteProgramadoQueNoAsistio),
                    PadLeft(dato.AtendidosEnSegundaEntrega)
                    );


            });


            datosObjetivo.Datos
                .Where(q => q.MunicipioAtencion == "Florencia"
                         && q.FechaRadicacion >= new DateTime(2016, 8, 1)
                         && q.FechaRadicacion <= new DateTime(2016, 8, 31)
                         && q.MotivoNoAtencionId == 377
                         )
                .ToList().ForEach(d =>
                {
                    Console.WriteLine(d.Id);
                });



            //SomeMethod(de);

            //var gr = de.GroupBy(f => f.FechaRadicacion.Value.ToString("yyyyMM"), new ClasificadorMess());
        }

        static string PadLeft( string dato)
        {
            return dato.PadLeft(12, ' ');
        }

        static string PadLeft( int dato)
        {
            return dato.ToString().PadLeft(4, ' ');
        }

        static T ReadFromFile<T>(string fileName)
        {
            var file = PathUtils.CombinePaths("~", "App_Data", fileName).MapHostAbsolutePath();
            Console.WriteLine("leyendo {0}", file);
            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var c = JsonSerializer.DeserializeFromStream<T>(fileStream);
                return c;
            }
        }

        static OrmLiteConnectionFactory SQLConnectionFactory(AppSettings appSettings)
        {
            var conexionBDSeguridad = appSettings.Get("ConexionGestionProyectos", Environment.GetEnvironmentVariable("APP_CONEXION_GP"));

            var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider);
           
            return dbfactory;
        }
    }


}

