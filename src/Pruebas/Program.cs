using GestionProyectos.CapaNegocios.Valores;
using GestionProyectos.Modelos.Peticiones.Declaraciones;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.IO;
using System.Linq;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("aqui vamos");

            var de = ReadFromFile<QueryResponse<DeclaracionesEstados>>("QueryDeclaracionesEstados.json").Results;
              // .Where(q => q.MunicipioAtencion == "Florencia" && q.FechaRadicacion >= new DateTime(2016, 6, 1)
              // ).ToList(); // && q.Id >= 49368).ToList();

            var tf = new FabricaTransformoFechas().TransformoFechas();
            var datosObjetivo = new FabricaDatosObjetivos().DatosObjetivoDosUno(de, tf);
            var reglas = new FabricaReglas().ReglasObjetivoDosUno();
            var r = new FabricaConstructores().ConstructorObjetivoDosUno(datosObjetivo, reglas, tf).Data;
            r.ForEach(dato =>
            {
                Console.WriteLine("{0} {1} {2} {3} d:{4} P:{5}  Q:{6}  R{7} S:{8} ",
                    dato.Periodo,  PadLeft(dato.Regional) , PadLeft(dato.Municipio), PadLeft(dato.Mes),
                    PadLeft(dato.Radicados),
                    PadLeft(dato.Excluidos),
                    PadLeft(dato.Elegibles),
                    PadLeft(dato.AtendidosPrimeraEntregaEnElMesDeRadicacion),
                    PadLeft(dato.AtendidosPrimeraEntregaRadicadosEnMesesAnteriores));

                if( dato.Municipio=="Florencia" && dato.Mes == "Junio")
                {
                    dato.MotivosExclusion.ForEach(m =>
                    {
                        Console.WriteLine("{0} {1} {2} ",  m.Id, m.Motivo, m.Cantidad);
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
                         && q.FechaRadicacion >= new DateTime(2016, 6, 1)
                         && q.FechaRadicacion <= new DateTime(2016, 6, 30)
                         && q.MotivoNoAtencionId==377
                         )
                .ToList().ForEach( d => {
                    Console.WriteLine(d.Id);
                });

            

            //SomeMethod(de);

            //var gr = de.GroupBy(f => f.FechaRadicacion.Value.ToString("yyyyMM"), new ClasificadorMess());

            Console.WriteLine("fin");
            Console.ReadLine();
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
            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                var c = JsonSerializer.DeserializeFromStream<T>(fileStream);
                return c;
            }
        }
    }


}

