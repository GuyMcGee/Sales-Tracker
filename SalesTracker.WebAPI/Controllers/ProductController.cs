using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _productService.CreateProductAsync(request))
            return Ok("Product created successfully.");

            return BadRequest("Product could not be created.");
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet, Route("GetProductBy{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
                return NotFound();
            else
                return Ok(product);
        }
        
        [HttpPut("UpdateProductBy{id}")]
        public async Task<IActionResult> EditProductById([FromBody] ProductEdit request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _productService.EditProductAsync(request)
                ? Ok("Game updated successfully.")
                : BadRequest("Game could note be updated.");
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> ProductDelete([FromRoute] int productId)
        {
            return await _productService.DeleteProductAsync(productId)
                ? Ok($"Note {productId} was deleted successfully.")
                : BadRequest($"Note {productId} could not be deleted.");
        }
    }
}