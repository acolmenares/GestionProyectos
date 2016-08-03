using System;
using Funq;

namespace GestionProyectos.Prueba
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var container = new Container();
			container.Register<int>(c => 2);

			container.Register<bool>(c => true);

			container.Register<string>(c => "hola");

			var v1 = container.Resolve<int>();
			var v2 = container.Resolve<bool>();
			var v3 = container.Resolve<string>();

			Console.WriteLine("{0} {1} {2}", v1, v2, v3);

			container.Register<int,string>((c,s) => int.Parse(s));
			var v4 = container.Resolve<int, string>("110");
			Console.WriteLine(v4);

			container.Register<string, string>((c, s) => s.ToUpper());
			var v5 = container.Resolve<string, string>("hola mundo");
			Console.WriteLine(v5);

			Console.WriteLine("fin");

		}
	}
}
