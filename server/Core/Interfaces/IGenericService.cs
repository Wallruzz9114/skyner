using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IGenericService<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetByIdWithObjectsAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAllWithObjectsAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}