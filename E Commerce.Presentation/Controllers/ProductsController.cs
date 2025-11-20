using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;
        public ProductsController( IProductService productService)
        {
            _productService = productService;
        }


        [RedisCache]
        [HttpGet]
        // GET : BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _productService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var Result = await _productService.GetProductByIdAsync(id);
            return HandleResult(Result);
        }

        // GetAllProductTypes
        // GET : BaseUrl/api/Products/types
        [HttpGet( "types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productService.GetAllTypesAsync();
            return Ok(Types);
        }

        // GetAllBrands 
        // GET : BaseUrl/api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _productService.GetAllBrandsAsync();
            return Ok( Brands);
        }

    }
}
