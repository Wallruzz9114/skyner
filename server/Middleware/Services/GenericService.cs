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

        public GenericService(DataContext dataContext) => _dataContext = dataContext;

        public void Add(T model) => _dataContext.Set<T>().Add(model);

        public async Task<int> CountAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).CountAsync();

        public void Delete(T model) => _dataContext.Set<T>().Remove(model);

        public async Task<T> GetByIdAsync(int id) => await _dataContext.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithObjectsAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAllAsync() => await _dataContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAllWithObjectsAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).ToListAsync();

        public void Update(T model)
        {
            _dataContext.Set<T>().Attach(model);
            _dataContext.Entry(model).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification) =>
            SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), specification);
    }
}