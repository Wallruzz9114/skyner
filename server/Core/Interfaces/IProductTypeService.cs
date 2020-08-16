using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<ProductType> GetProductTypeByIdAsync(int id);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}