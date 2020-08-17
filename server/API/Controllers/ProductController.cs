using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
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

        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<ProductViewModel>>> GetProductsAsync()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productService.ListAllWithObjectsAsync(specification);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductViewModel>>(products));
        }

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

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _productBrandService.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _productTypeService.ListAllAsync());
        }
    }
}