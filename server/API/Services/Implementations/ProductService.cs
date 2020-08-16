using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Middleware.Data;

namespace API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dataContext.Products
                .Include(product => product.ProductBrand)
                .Include(product => product.ProductType)
                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _dataContext.Products
                .Include(product => product.ProductBrand)
                .Include(product => product.ProductType)
                .ToListAsync();
        }
    }
}