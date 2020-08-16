using Core.Models;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : Specification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}