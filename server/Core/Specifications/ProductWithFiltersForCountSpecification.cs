using Core.Models;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : Specification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductResultParameters parameters)
            : base(product =>
                (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains(parameters.Search)) &&
                (!parameters.BrandId.HasValue || product.ProductBrandId == parameters.BrandId) &&
                (!parameters.TypeId.HasValue || product.ProductTypeId == parameters.TypeId)
            )
        { }
    }
}