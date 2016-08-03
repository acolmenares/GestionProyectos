using GestionProyectos.Modelos.Interfaces;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.CAD
{
    public class RepoHerramientaEnArchivos : IRepoHerramienta
    {

        public string DirectorioRaiz { get; protected set; }
       
        public RepoHerramientaEnArchivos()
        {
            DirectorioRaiz = PathUtils.CombinePaths("~", "App_Data", "Herramienta").MapHostAbsolutePath();
            try
            {
                Directory.CreateDirectory(DirectorioRaiz);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public T Consultar<T>(string key, Func<T> tFunc)
        {
            var fn = NombreCompletoJson(key);
            return (File.Exists(fn)) ? ReadFromFile<T>(fn) : tFunc();
        }

        public void Crear<T>(string key, T data)
        {

            var fn = NombreCompletoJson(key);
            SaveToFile(data, fn);
        }
                

        private string NombreCompletoJson(string key)
        {
            return PathUtils.CombinePaths(DirectorioRaiz, key + ".json");
        }



        private bool ExisteJson(string key)
        {
            return File.Exists(NombreCompletoJson(key));
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

        private static void SaveToFile<T>(T data, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.SerializeToStream<T>(data, fileStream);
            }
        }

    }
}
