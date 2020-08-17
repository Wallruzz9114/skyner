using System.Linq;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Data
{
    public class SpecificationEvaluator<T> where T : BaseModel
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Creteria != null)
                query = query.Where(specification.Creteria);
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
            if (specification.IsPagingEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);

            return specification.Includes.Aggregate(query, (model, expression) => model.Include(expression));
        }
    }
}