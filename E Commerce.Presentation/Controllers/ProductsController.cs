using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductsDTOs;
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

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController( IProductService productService)
        {
            _productService = productService;
        }

        // GetAllProducts (from image_0c03d7.png)
        [HttpGet]
        // GET : BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var Products = await _productService.GetAllProductsAsync();
            return Ok(Products);
        }

        // GetProductById 
        [HttpGet(  "{id}")]
        // GET : BaseUrl/api/Products/1
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var Product = await _productService.GetProductByIdAsync(id);
            return Ok(Product);
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
