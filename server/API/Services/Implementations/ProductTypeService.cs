using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace API.Services.Implementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly DataContext _dataContext;

        public ProductTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            return await _dataContext.ProductTypes.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _dataContext.ProductTypes.ToListAsync();
        }
    }
}