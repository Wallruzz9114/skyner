using System;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericService<TModel> Service<TModel>() where TModel : BaseModel;
        Task<int> SaveChangesToDatabase();
    }
}