using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Services
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