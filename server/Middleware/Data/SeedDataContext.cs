using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Middleware.Data
{
    public class SeedDataContext
    {
        public static async Task SeedDataAsync(DataContext dataContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!dataContext.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText("../Middleware/Data/Seed/product_brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);

                    foreach (var productBrand in productBrands)
                    {
                        dataContext.ProductBrands.Add(productBrand);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText("../Middleware/Data/Seed/product_types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

                    foreach (var productType in productTypes)
                    {
                        dataContext.ProductTypes.Add(productType);
                    }

                    await dataContext.SaveChangesAsync();
                }

                if (!dataContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Middleware/Data/Seed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                    {
                        dataContext.Products.Add(product);
                    }

                    await dataContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger<SeedDataContext>();
                logger.LogError(exception.Message);
            }
        }
    }
}