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

            return specification.Includes.Aggregate(query, (model, expression) => model.Include(expression));
        }
    }
}