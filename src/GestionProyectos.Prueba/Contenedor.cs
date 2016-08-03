using System;
using System.Collections.Generic;

namespace GestionProyectos.Prueba
{
	public class Contenedor
	{
		//Dictionary<string, string> servicios;

		//Func<string> x = () => "";

		public Contenedor()
		{
			var type = typeof(Func<string>);
			var type2 = typeof(Func<bool>);
			var type3 = typeof(Func<int>);
			var type4 = typeof(Func<int, string>);
			var type5 = typeof(Func<int, int, string>);
			Console.WriteLine(type);
			Console.WriteLine(type2);
			Console.WriteLine(type3);
			Console.WriteLine(type4);
			Console.WriteLine(type5);
		}


		public void Registrar()
		{
		}
	}
}

