using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<ProductBrand> _productBrandService;
        private readonly IGenericService<ProductType> _productTypeService;
        private readonly IMapper _mapper;

        public ProductController(
            IGenericService<Product> productService,
            IGenericService<ProductBrand> productBrandService,
            IGenericService<ProductType> productTypeService,
            IMapper mapper)
        {
            _productTypeService = productTypeService;
            _productBrandService = productBrandService;
            _productService = productService;
            _mapper = mapper;
        }

        [Cached(600)]
        [HttpGet("all")]
        public async Task<ActionResult<Pagination<ProductViewModel>>> GetProductsAsync(
            [FromQuery] ProductResultParameters parameters)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(parameters);
            var countSpecification = new ProductWithFiltersForCountSpecification(parameters);
            var totalItems = await _productService.CountAsync(countSpecification);
            var products = await _productService.ListAllWithObjectsAsync(specification);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products);

            return Ok(new Pagination<ProductViewModel>(parameters.PageIndex, parameters.PageSize, totalItems, data));
        }

        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductViewModel>> GetProductAsync(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productService.GetByIdWithObjectsAsync(specification);

            if (product == null)
                return NotFound(new APIResponse(404));

            return _mapper.Map<Product, ProductViewModel>(product);
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync() =>
            Ok(await _productBrandService.ListAllAsync());

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync() =>
            Ok(await _productTypeService.ListAllAsync());
    }
}