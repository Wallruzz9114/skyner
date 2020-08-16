using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseModel
    {
        private readonly DataContext _dataContext;

        public GenericService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithObjectsAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllWithObjectsAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), specification);
        }
    }
}