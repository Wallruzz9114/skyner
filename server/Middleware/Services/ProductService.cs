using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace Middleware.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext) => _dataContext = dataContext;

        public async Task<Product> GetProductByIdAsync(int id) =>
            await _dataContext.Products
                .Include(product => product.ProductBrand)
                .Include(product => product.ProductType)
                .FirstOrDefaultAsync(product => product.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync() =>
            await _dataContext.Products
                .Include(product => product.ProductBrand)
                .Include(product => product.ProductType)
                .ToListAsync();
    }
}