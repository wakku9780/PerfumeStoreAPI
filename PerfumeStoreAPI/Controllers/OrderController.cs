using Microsoft.AspNetCore.Mvc;
using PerfumeStoreAPI.DTOs;
using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Status = orderDto.Status,
                OrderItems = orderDto.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            var createdOrderDto = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrderDto.Id }, createdOrderDto);
        }



        // POST: api/Order
        // POST: api/Order
        //[HttpPost]
        //public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto) // CreateOrderDto ya updated OrderDto use karo
        //{
        //    var order = new Order
        //    {
        //        UserId = orderDto.UserId,
        //        OrderDate = orderDto.OrderDate,
        //        TotalAmount = orderDto.TotalAmount,
        //        Status = orderDto.Status,
        //        OrderItems = orderDto.OrderItems.Select(item => new OrderItem
        //        {
        //            ProductId = item.ProductId,
        //            Quantity = item.Quantity,
        //            UnitPrice = item.UnitPrice
        //        }).ToList()
        //    };

        //    await _orderService.CreateOrderAsync(order);
        //    return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        //}


        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
