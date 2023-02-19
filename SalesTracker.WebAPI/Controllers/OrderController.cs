using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesTracker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> OrderCreate(OrderCreate orderCreate)
        {
             return Ok(await _orderService.CreateOrder(orderCreate));
            
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

       [HttpGet, Route("GetOrderBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order is null)
                return NotFound();
            else
                return Ok(order);
        }

        [HttpDelete, Route("DeleteOrderBy{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _orderService.DeleteOrder(id))
                return Ok("Successful Deletion.");
            else
                return StatusCode(500, "Internal Server Error.");
        }

        [HttpPut, Route("UpdateOrdersBy{id}")]
        public async Task<IActionResult> EditOrder(int id, OrderEdit updateorder)
        {

            var order = await _orderService.EditOrder(id, updateorder);
            if (order is null)
                return NotFound();
            else
                return Ok(order);
        }
    }
}