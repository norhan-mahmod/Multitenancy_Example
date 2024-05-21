using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multitenancy.Dtos;

namespace Multitenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await productService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetAsync(int productId)
        {
            var product = await productService.GetByIdAsync(productId);
            return product is null ? NotFound() : Ok(product);
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateAsync(CreateProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Rate = productDto.Rate
            };
            var result = await productService.CreateAsync(product);
            return Ok(result);
        }

    }
}
