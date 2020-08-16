using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace API.Services.Implementations
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly DataContext _dataContext;

        public ProductBrandService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            return await _dataContext.ProductBrands.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _dataContext.ProductBrands.ToListAsync();
        }
    }
}