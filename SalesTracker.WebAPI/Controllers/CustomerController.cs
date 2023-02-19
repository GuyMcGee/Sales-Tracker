using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Create Customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerCreateResult = await _customerService.CustomerCreateAsync(model);
            if(customerCreateResult)
            {
                return Ok("Customer was created");
            }
            return BadRequest("Customer could not be created");
        }

        [HttpGet,Route("{customerId}")]
        public async Task<IActionResult> GetById([FromRoute] int customerId)
        {
            var customerDetail = await _customerService.GetCustomerByIdAsync(customerId);
            if (customerDetail is null)
            {
                return NotFound();
            }
            return Ok(customerDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _customerService.GetCustomersAsync());
        }

        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            return await _customerService.DeleteCustomerAsync(customerId)
                ? Ok("Customer was deleted")
                : BadRequest("Customer could not be deleted");
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer( int id, CustomerEdit model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest(ModelState);
            }
            if (await _customerService.UpdateCustomerAsync(id, model))
                return Ok("success");
                else
                return UnprocessableEntity();
        }
    }
}