using GestionProyectos.Modelos.Interfaces;
using ServiceStack.Data;
using ServiceStack.Model;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using ServiceStack;
using ServiceStack.Web;
using GestionProyectos.Modelos.Entidades;
using ServiceStack.Caching;
using GestionProyectos.Modelos.Peticiones.Declaraciones;

namespace GestionProyectos.CAD
{
	public partial class RepositorioBD:IRepositorio
    {
        protected IDbConnection conexion;
        protected IDbTransaction transaccion = null;
		
        public RepositorioBD(IDbConnectionFactory dbConnectionFactory, bool crearTransaccion = false)
        {
            conexion = dbConnectionFactory.Open();
			if (crearTransaccion) Execute(con => transaccion = con.OpenTransaction());
        }
        
        public List<T> Consultar<T>(Expression<Func<T, bool>> predicate)
        {
            return Execute<List<T>>(con => con.Select(predicate));
        }

        public T Consultar<T>(int id) where T : IEntidad
        {
            return Execute<T>(con => con.SingleById<T>(id));
        }

        public List<T> Consultar<T>() 
        {
            return Execute<List<T>>(con => con.Select<T>());
        }

        public T ConsultarSimple<T>(Expression<Func<T, bool>> predicate)
        {
            return Execute<T>(con => con.Single(predicate));
        }
                       

        public int Actualizar<T>(T data) where T : IEntidad
        {
            int r = 0;
            Execute(con => {
                r = con.Update<T>(data, f => f.Id == data.Id);
            });
            return r;
        }

        public int Actualizar<T>(T data, Expression<Func<T, Object>> onlyFields, Expression<Func<T, bool>> predicate)
        {
            int c = 0;
            Execute(con => {
				c = con.UpdateOnly<T>(data, onlyFields, predicate);
            });
            return c;
        }

		public int Actualizar<T>(T data, Expression<Func<T, object>>onlyFields) where T : IEntidad
        {
            int c = 0;
            Execute(con => {
				c = con.UpdateOnly<T>(data, onlyFields , q => q.Id == data.Id );
            });
            return c;
        }

        public int Actualizar<T>(T data, Expression<Func<T, bool>> predicate)
        {
            int c = 0;
            Execute(con => {
                var updateOnly = con.From<T>().Where(predicate);
                c = con.UpdateOnly<T>(data, updateOnly);
            });

            return c;
        }
             

        public void Crear<T>(T data) where T : IEntidad
        {
            Execute(con => {
                con.Insert(data);
                data.Id = int.Parse(con.LastInsertId().ToString());
                Console.WriteLine(data.Id);
            });
        }

        public int Borrar<T>(int id) where T : IHasId<int>
        {
            int c = 0;
            Execute(con => {
                c= con.DeleteById<T>(id);
            });
            return c;
        }

        public int Borrar<T>(T data) where T : IHasId<int>
        {
            return Borrar<T>(data.Id);
        }

        public int Borrar<T>(Expression<Func<T, bool>> predicate)
        {
            int c = 0;
            Execute(con => {
                c = con.Delete<T>(predicate);
            });
            return c;
        }



        public void IniciarTransaccion()
        {
            if (transaccion != null)
            {
                Execute(con => transaccion = con.OpenTransaction());
            }
        }

        public void FinalizarTransaccion()
        {
            if (transaccion != null)
            {
                transaccion.Commit();
                transaccion.Dispose();
                transaccion = null;
            }
        }

        public void CancelarTransaccion()
        {
            Rollback();
        }
        
        public void Dispose()
        {
            Rollback();
            Execute(con => con.Dispose());
        }


        private void Rollback()
        {
            if (transaccion != null)
            {
                transaccion.Rollback();
                transaccion.Dispose();
                transaccion = null;
            }
        }

        protected  void Execute(Action<IDbConnection> acciones)
        {
            acciones(conexion);
        }

        protected  T Execute<T>(Func<IDbConnection, T> acciones)
        {
            return acciones(conexion);
        }
        

        private SqlExpression<T> CrearSqlExpression<T>()
        {
			return conexion.From<T>();
        }

        
        /*
        public List<dynamic> Consultar<T>(SqlExpression<T> consulta)
        {
            List<dynamic> r= new List<dynamic>();
            Execute(cn =>
            {
                r = cn.Select<dynamic>(consulta);
            });
            return r;
        }
*/


    }
}
