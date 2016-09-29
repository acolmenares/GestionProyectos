using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Peticiones.Objetivos;
using GestionProyectos.Modelos.Peticiones.Declaraciones;

namespace GestionProyectos.CAD
{
    public class RepoHerramientaEnArchivos : IRepoHerramienta
    {
        private DirectoryInfo _dirraiz;
        private DirectoryInfo metaDir;

        public ITransformoFechas TransformoFechas { get; set; }

        public string DirectorioRaiz
        {
            get
            {
                return _dirraiz.FullName;
            }
            set
            {
                _dirraiz = new DirectoryInfo(value);
                _dirraiz.Create();
                metaDir = _dirraiz.CreateSubdirectory("Meta");

            }
        }

        public RepoHerramientaEnArchivos()
        {
            DirectorioRaiz = PathUtils.CombinePaths("~", "App_Data", "Herramienta").MapHostAbsolutePath();

        }


        public T Consultar<T>(string rango, Func<T> tFunc)
        {
            var fn = NombreCompletoJson<T>(rango);
            return (File.Exists(fn)) ? ReadFromFile<T>(fn) : tFunc();
        }

        public List<T> Consultar<T>(string rango, Func<List<T>> tListFunc)
        {
            var fn = NombreCompletoJson<T>(rango);
            return (File.Exists(fn)) ? ReadListFromFile<T>(fn) : tListFunc();
        }

        public void Crear<T>(string rango,Func<T> dataFunc)
        {
            ObtenerDirectorioLanzarErrorSiNoExiste(rango);
            var data = dataFunc();
            var fn = NombreCompletoJson<T>(rango);
            SaveToFile(data, fn);
        }

        public void BorrarRango(string rango)
        {
            var _dirrango = ObtenerDirectorioLanzarErrorSiNoExiste(rango);
            _dirrango.Delete(true);
        }

        
        public void CrearRango(ITengoFechaRadicacionDesdeHasta query, Func<TablasRango> tablasFunc)
        {
            var rango = TransformoFechas.ConvertirEnRango(query);
            var _dirrango = new DirectoryInfo(PathUtils.CombinePaths(_dirraiz.FullName, rango));
            if ( _dirrango.Exists)
            {
                throw new Exception(string.Format("Rango '{0}' ya existe. Debe ser borrado primero para volver a crearlo", rango));                              
            }

            _dirrango.Create();

            var tablas = tablasFunc();

            CrearRangoImp(rango, tablas.SubTablas, sololectura:true);
            CrearRangoImp(rango, tablas.Declaracion, sololectura: true);
            CrearRangoImp(rango, tablas.DeclaracionEstados, sololectura: true);
            CrearRangoImp(rango, tablas.Personas, sololectura: true);
            CrearRangoImp(rango, tablas.PersonasContactos, sololectura: true);
            CrearRangoImp(rango, tablas.Programacion, sololectura: true);

            CrearMeta(rango, tablas, query);
        }

        private void CrearMeta(string rango, TablasRango tablas, ITengoFechaRadicacionDesdeHasta query)
        {
            var regionales = tablas.Declaracion.GroupBy(r => r.Id_Regional).Select(reg => new RegionalObjetivo
            {
                Nombre = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
                Municipios = reg.GroupBy(mn => mn.Id_lugar_fuente).Select(municipio => new MunicipioObjectivo {
                    Nombre = tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
                    Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
                    AniosMesesRadicacion = municipio.GroupBy(per => TransformoFechas.ConvertirEnAnioMes(per.Fecha_Radicacion)).Select(periodo => new AnioMesObjetivo
                    {
                        Nombre= periodo.Key,
                        Municipio= tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
                        Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
                    }).OrderBy(x=>x.Nombre).ToList(),
                    AniosMesesAtencion = municipio.Where(q=>q.Fecha_Valoracion.HasValue).GroupBy(per => TransformoFechas.ConvertirEnAnioMes(per.Fecha_Valoracion))
                    .Select(periodo => new AnioMesObjetivo
                    {
                        Nombre = periodo.Key,
                        Municipio = tablas.SubTablas.Find(st => st.Id == municipio.Key).Descripcion,
                        Regional = tablas.SubTablas.Find(st => st.Id == reg.Key).Descripcion,
                    }).OrderBy(x => x.Nombre).ToList()
                }).ToList() 
            }).ToList();

            var municipios = new List<string>();
            regionales.ForEach(reg => {
                municipios.AddRange(reg.Municipios.GroupBy(g => g.Nombre).Select(q => q.Key));
            });

            var ri = new Rango
            {
                Regionales = regionales,
                Municipios = municipios,
                Nombre= rango,
                Periodo= TransformoFechas.ConvertirRangoEnPeriodo(rango),
                AnioMes= TransformoFechas.ConvertirRangoEnAnioMes(rango),
                Fecha_RadicacionGreaterThanOrEqualTo= query.Fecha_RadicacionGreaterThanOrEqualTo,
                Fecha_RadicacionLessThanOrEqualTo= query.Fecha_RadicacionLessThanOrEqualTo
            };


            var rangos = ConsultarRango();
            var i = rangos.FindIndex(q => q.Nombre == rango);
            if (i >= 0)
            {
                rangos.RemoveAt(i);
            }
            rangos.Add(ri);

            GuardarRangos(rangos);
        }

        public List<Rango> ConsultarRango()
        {
            var fn = NombreEnMetDir<Rango>();
            return File.Exists(fn)? ReadListFromFile<Rango>(fn): new List<Rango>();
        }

        public List<Rango> ConsultarRango(ConsultarRango peticion)
        { 
            return ConsultarRango();
        }

        private void GuardarRangos(List<Rango> rangos)
        {
            var fn = NombreEnMetDir<Rango>();
            SaveToFile(rangos.OrderByDescending(q=>q.Nombre).ToList(), fn, sobreescribir: true);
        }


        private void CrearRangoImp<T>(string rango, List<T> data, bool sololectura=false)
        {
            var fn = NombreCompletoJson<T>(rango);
            SaveToFile(data, fn,sololectura:sololectura);
        }

        private string NombreCompletoJson<T>(string rango)
        {
            
            var type = typeof(T);
            var name = type.Name.StartsWith("QueryResponse") ?"Query"+ type.GetGenericArguments()[0].Name : type.Name;
            
            return PathUtils.CombinePaths(DirectorioRaiz, 
                rango.StartsWith(name)? rango.ReplaceFirst(name,""): rango,
                name + ".json");
        }

        private string NombreEnMetDir<T>()
        {
            return PathUtils.CombinePaths(metaDir.FullName, typeof(T).Name + ".json");
        }


        private bool ExisteJson<T>(string rango)
        {
            return File.Exists(NombreCompletoJson<T>(rango));
        }


        private static T ReadFromFile<T>(string fileName)
        {
            var r = default(T);
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                r = JsonSerializer.DeserializeFromStream<T>(fileStream);
            }
            return r;
        }

        private static List<T> ReadListFromFile<T>(string fileName)
        {
            var r = new List<T>();
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                r = JsonSerializer.DeserializeFromStream<List<T>>(fileStream);
            }
            return r;
        }


        private static void SaveToFile<T>(T data, string fileName, bool sobreescribir=false, bool sololectura=false)
        {
            if( sobreescribir || ! File.Exists(fileName))
            {
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializer.SerializeToStream(data, fileStream);
                }
                if(sololectura)
                {
                    File.SetAttributes(fileName, FileAttributes.ReadOnly);
                }
            }
            
        }

        private  DirectoryInfo ObtenerDirectorioLanzarErrorSiNoExiste(string rango)
        {
            var _dirrango = new DirectoryInfo(PathUtils.CombinePaths(_dirraiz.FullName, rango));
            if (!_dirrango.Exists)
            {
                throw new Exception(string.Format("Rango '{0}' NO ya existe. verifique las fechas", rango));
            }
            return _dirrango;
        }

        //public QueryResponse<DeclaracionesEstados> QueryDeclaracionesEstados(string rango, Func<QueryResponse<DeclaracionesEstados>> dataFunc)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
