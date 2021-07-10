using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Filters
{
    public interface IFilter<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }
}