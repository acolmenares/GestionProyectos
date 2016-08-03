using System;
using GestionProyectos.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace GestionProyectos.Modelos.Entidades
{
	public class Persona : IEntidad
	{
		public Persona()
		{
		}
		[AutoIncrement]
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
	}			
}

