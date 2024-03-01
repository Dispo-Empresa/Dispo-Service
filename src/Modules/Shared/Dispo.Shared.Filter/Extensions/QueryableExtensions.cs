using Dispo.Shared.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Shared.Filter.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query, List<string> includes) where T : EntityBase
        {
            if (!includes.Any())
                return query;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
