using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Filter.Model;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dispo.Shared.Filter.Services
{
    public class FilterService : IFilterService
    {
        private readonly DispoContext _dispoContext;

        public FilterService(DispoContext dispoContext)
        {
            _dispoContext = dispoContext;
        }

        public List<T> Get<T>(FilterModel filterModel) where T : EntityBase
        {
            return _dispoContext.Set<T>()
                                .AsNoTracking()
                                .Where(BuildExpression<T>(filterModel))
                                .ToList();
        }

        private Func<T, bool> BuildExpression<T>(FilterModel filterModel)
        {
            var parameter = Expression.Parameter(typeof(T), filterModel.Entity);
            Expression filterExpression = null;
            foreach (var property in filterModel.Properties)
            {
                var memberExpression = Expression.Property(parameter, property.Name);
                var constant = Expression.Constant(property.Value);
                Expression comparison;
                if (memberExpression.Type == typeof(string))
                {
                    comparison = Expression.Call(memberExpression, "Contains", Type.EmptyTypes, constant);
                }
                else
                {
                    constant = Expression.Constant(Convert.ToInt32(property.Value));
                    comparison = Expression.Equal(memberExpression, constant);
                }

                filterExpression = filterExpression == null ? comparison : Expression.And(filterExpression, comparison);
            }

            return Expression.Lambda<Func<T, bool>>(filterExpression, parameter).Compile();
        }
    }
}
