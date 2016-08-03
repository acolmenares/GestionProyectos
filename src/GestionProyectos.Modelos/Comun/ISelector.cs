using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GestionProyectos.Modelos.Comun
{
    public interface ISelector<From>
    {
        Expression<Func<From, object>> Fields { get; set; }
        IEnumerable<int> InValues { get; set; }
        Expression<Func<From, bool>> Predicate { get; set; }
        Expression<Func<From, object>> FieldForInValues { get; set; }
        Expression<Func<From, object>> OrderBy { get; set; }
        Expression<Func<From, object>> GroupBy { get; set; }

    }
}