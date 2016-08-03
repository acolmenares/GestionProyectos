namespace GestionProyectos.Modelos.Interfaces
{ }

    /*
        public interface IConsulta
        {
            string Fields { get; set; }
            string OrderBy { get; set; }
            string OrderByDesc { get; set; }
            int? Skip { get; set; }
            int? Take { get; set; }
        }

        public interface IConsulta<From> : IConsulta { }

        public interface IConsulta<From,Into> : IConsulta { }

        public interface IConsultaResponse: IHasResponseStatus
        {
            int Offset { get; set; }
            int Total { get; set; }
        }

        [DataContract]
        public class ConsultaResponse<T> : IConsultaResponse, IHasResponseStatus
        {
            public ConsultaResponse() {
                Results = new List<T>();
            }

            [DataMember(Order = 1)]
            public virtual int Offset { get; set; }

            [DataMember(Order = 2)]
            public virtual int Total { get; set; }

            [DataMember(Order = 3)]
            public virtual List<T> Results { get; set; }

            [DataMember(Order = 4)]
            public virtual ResponseStatus ResponseStatus { get; set; }
        }

        public abstract class ConsultaBase : IConsulta
        {
            [DataMember(Order = 1)]
            public virtual int? Skip { get; set; }

            [DataMember(Order = 2)]
            public virtual int? Take { get; set; }

            [DataMember(Order = 3)]
            public virtual string OrderBy { get; set; }

            [DataMember(Order = 4)]
            public virtual string OrderByDesc { get; set; }

            [DataMember(Order = 5)]
            public virtual string Fields { get; set; }

        }

        public abstract class Consulta<T> : ConsultaBase, IConsulta<T>, IReturn<ConsultaResponse<T>> { }
        public abstract class Consulta<From, Into> : ConsultaBase, IConsulta<From, Into>, IReturn<ConsultaResponse<Into>> { }
        */
