using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace API.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}