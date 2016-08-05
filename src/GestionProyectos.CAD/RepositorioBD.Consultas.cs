using GestionProyectos.Modelos.Comun;
using GestionProyectos.Modelos.Entidades;
using GestionProyectos.Modelos.Interfaces;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionProyectos.CAD
{
    public partial class RepositorioBD : IRepositorio
	{
        private const int MaxLengthForInValues= 2000;

		public List<DescripcionDeclaracion> ConsultarDescripcionDeclaracion<T>(IEnumerable<int> idsDeclaraciones)
			where T : IDeclaracionConIdSubTabla, IConIdDeclaracion
		{
            var qr = CrearSqlExpression<T>()
              .LeftJoin<SubTablas>((declaracion, st) => declaracion.Id_SubTabla == st.Id)
                .Where(q => Sql.In(q.Id_Declaracion, idsDeclaraciones));

			var r = new List<DescripcionDeclaracion>();
			Execute(cn =>
			{
				r = cn.Select<DescripcionDeclaracion>(qr);
			});
			return r;
		}
          
        
        public List<PersonasContactos> ConsultarPersonasContactos(IEnumerable<int> idsDeclaraciones, Expression<Func<PersonasContactos,object>> fields=null)
        {
            return Consultar(new Selector<PersonasContactos> { InValues= idsDeclaraciones, FieldForInValues= f=>f.Id_Persona, Fields=fields});               
        }

        public List<PersonasPorDeclaracion> ContarPersonasPorDeclaracion(IEnumerable<int> idsDeclaraciones)
        {
            return Consultar<Personas, PersonasPorDeclaracion>(new Selector<Personas> {
                InValues = idsDeclaraciones,
                Fields= q => new { q.Id_Declaracion, Cantidad = Sql.As(Sql.Count(q.Id), "Cantidad") },
                GroupBy = q => q.Id_Declaracion
            });
            
        }



        public List<Declaracion> ConsultarDeclaracion(ITengoFechaRadicacionDesdeHasta modelo) 
        {
            var predicate = BuildPredicate(modelo);
            var qr = CrearSqlExpression<Declaracion>().Where(predicate);
            
            var r = new List<Declaracion>();
            Execute(cn =>
            {
                r = cn.Select(qr);
            });

            return r;
        }

        public List<Personas> ConsultarPersonasDeclaracion(IEnumerable<int> idsDeclaraciones, string tipoPersona=null)
        {
            var selector = new Selector<Personas> {
                InValues = idsDeclaraciones,
                FieldForInValues = f => f.Id_Declaracion,
            };
            if (!string.IsNullOrEmpty(tipoPersona))
                selector.Predicate = q => q.Tipo == tipoPersona;

            return Consultar(selector);
                        
        }

        public List<DeclaracionEstados> ConsultarDeclaracionEstados(List<int> idsDeclaraciones, Expression<Func<DeclaracionEstados, object>> fields = null)
        {
            return Consultar(new Selector<DeclaracionEstados> {
                InValues = idsDeclaraciones,
                FieldForInValues = f => f.Id_Declaracion,
                Fields = fields
            });
                        
        }
        

        // AQUI punto de retorno 1
        public List<From> Consultar<From>(ISelector<From> selector)
        {
            return Consultar<From, From>(selector);
        }

        public List<Into> Consultar<From,Into>(ISelector<From> selector)
        {
            var r = new List<Into>();
            
            
            if (selector.InValues != null  )
            {
                var fieldForInValue = "";
                if (selector.FieldForInValues != null)
                {
                    var def = OrmLiteUtils.GetModelDefinition(typeof(From)).GetFieldDefinition<From>(selector.FieldForInValues);
                    fieldForInValue = def.FieldName;
                }

                var ids = Split(selector.InValues);
                foreach (var idList in ids)
                {
                    var qr = CrearSqlExpression(selector, false);

                    if (selector.Predicate != null)
                        qr.Where().Where(selector.Predicate).And(fieldForInValue+ " in ({0})",  new SqlInValues(idList));
                    else
                        qr.Where().Where(fieldForInValue + " in ({0})", new SqlInValues(idList)); 

                    Execute(cn =>
                    {
                        r.AddRange(cn.Select<Into>(qr));
                    });
                }
            }
            else
            {
                var qr = CrearSqlExpression(selector, true);
                Execute(cn =>
                {
                    r = cn.Select<Into>(qr);
                });
            }
            return r;
        }

        private  SqlExpression<From> CrearSqlExpression<From>(ISelector<From> selector, bool includePredicate)
        {
            var qr = CrearSqlExpression<From>();

            if (selector.Fields != null)
                qr.Select(selector.Fields);
            if (selector.OrderBy != null)
                qr.OrderBy(selector.OrderBy);
            if (selector.GroupBy != null)
                qr.GroupBy(selector.GroupBy);

            if (includePredicate && selector.Predicate != null)
                qr.Where(selector.Predicate);
            
            return qr;
        }

        private static Expression<Func<Declaracion, bool>> BuildPredicate(ITengoFechaRadicacionDesdeHasta modelo) 
        {
            
            var predicate = PredicateBuilder.True<Declaracion>();

            if (modelo.Fecha_RadicacionGreaterThanOrEqualTo.HasValue)
            {
                var f1 = modelo.Fecha_RadicacionGreaterThanOrEqualTo.Value;
                predicate = predicate.And(q => q.Fecha_Radicacion >= f1);
            }

            if (modelo.Fecha_RadicacionLessThanOrEqualTo.HasValue)
            {
                var f2 = modelo.Fecha_RadicacionLessThanOrEqualTo.Value;
                predicate = predicate.And(q => q.Fecha_Radicacion <= f2);
            }
            
            return predicate;
        }

        private static List<List<int>> Split(IEnumerable<int> source, int maxSubItems= MaxLengthForInValues)
        {
            return source
                 .Select((x, i) => new { Index = i, Value = x })
                 .GroupBy(x => x.Index / maxSubItems)
                 .Select(x => x.Select(v => v.Value).ToList())
                 .ToList();
        }

    }
}

