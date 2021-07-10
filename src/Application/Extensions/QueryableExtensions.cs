using Application.Filters;
using Microsoft.EntityFrameworkCore;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;
            
            int count = await query.CountAsync();
            List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, IFilter<T> filter) where T : class
        {
            var queryableResultWithIncludes = filter.Includes.Aggregate(query, (current, include) => current.Include(include));
            var secondaryResult = filter.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return secondaryResult.Where(filter.Criteria);
        }
    }
}