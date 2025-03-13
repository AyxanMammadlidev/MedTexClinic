using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            var products = await _productService.GetAllAsync(page, take);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto productDto)
        {
            await _productService.CreateAsync(productDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto productDto)
        {
            if (id < 1)
                return BadRequest();
            await _productService.UpdateAsync(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _productService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("filter-by-price")]

        public async Task<IActionResult> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = await _productService.FilterByPriceAsync(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpGet("price-decending-order")]

        public async Task<IActionResult> PriceByDecending(int page = 1, int take = 3)
        {
            var products = await _productService.GetProductsByPriceDescending(page, take);
            return Ok(products);
        }

        [HttpGet("price-ascending-order")]
        public async Task<IActionResult> PriceByAscending(int page = 1, int take = 3)
        {
            var products = await _productService.GetProductsByPriceAscending(page, take);
            return Ok(products);
        }

        [HttpGet("sort-by-rating")]
        public async Task<IActionResult> SortByRating(int page = 1, int take = 3)
        {
            var products = await _productService.GetProductsByRating(page, take);
            return Ok(products);
        }

    }
}
