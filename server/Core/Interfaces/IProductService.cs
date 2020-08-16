using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}