using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace API.Services.Interfaces
{
    public interface IProductBrandService
    {
        Task<ProductBrand> GetProductBrandByIdAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}