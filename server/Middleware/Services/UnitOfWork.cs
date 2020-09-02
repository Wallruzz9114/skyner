using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Middleware.Data;

namespace Middleware.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private Hashtable _services;

        public UnitOfWork(DataContext dataContext) => _dataContext = dataContext;

        public void Dispose() => _dataContext.Dispose();

        public async Task<int> SaveChangesToDatabase() => await _dataContext.SaveChangesAsync();

        public IGenericService<TModel> Service<TModel>() where TModel : BaseModel
        {
            if (_services == null) _services = new Hashtable();

            var modelName = typeof(TModel).Name;

            if (!_services.ContainsKey(modelName))
            {
                var serviceType = typeof(GenericService<>);
                var serviceInstance = Activator.CreateInstance(serviceType.MakeGenericType(typeof(TModel)), _dataContext);
                _services.Add(modelName, serviceInstance);
            }

            return (IGenericService<TModel>)_services[modelName];
        }
    }
}