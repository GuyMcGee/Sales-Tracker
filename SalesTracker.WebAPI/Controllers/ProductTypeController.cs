using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
                _productTypeService = productTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductType([FromBody] ProductTypeCreate request)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            if (await _productTypeService.CreateProductTypeAsync(request))
            return Ok("Product created successfully.");

            return BadRequest("Product could not be created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductType()
        {
            var productTypes = await _productTypeService.GetAllProductTypeAsync();
            return Ok(productTypes);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetProductTypeById(int id)
        {
            var productType = await _productTypeService.GetProductTypeByIdAsync(id);
            if (productType is null)
                return NotFound();
            else
                return Ok(productType);
        }

        [HttpPut]
        public async Task<IActionResult> EditProductTypeById([FromBody] ProductTypeEdit request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return await _productTypeService.EditProductTypeAsync(request)
                ? Ok("Game updated successfully.")
                : BadRequest("Game could note be updated.");
        }

        [HttpDelete("{productTypeId:int}")]
        public async Task<IActionResult> DeleteProductType([FromRoute] int productTypeId)
        {
            return await _productTypeService.DeleteProductTypeAsync(productTypeId)
                ? Ok($"Note {productTypeId} was deleted successfully.")
                : BadRequest($"Note {productTypeId} could not be deleted.");
        }
    }
}