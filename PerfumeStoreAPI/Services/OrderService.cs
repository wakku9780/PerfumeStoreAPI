using PerfumeStoreAPI.DTOs;
using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            // Map each order to OrderDto
            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                User = new UserDto
                {
                    Id = order.User.Id,
                    Name = order.User.Name,
                    Email = order.User.Email
                },
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Product = new ProductDto
                    {
                        Id = oi.Product.Id,
                        Name = oi.Product.Name,
                        Price = oi.Product.Price,
                        Description = oi.Product.Description
                    }
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                User = new UserDto
                {
                    Id = order.User.Id,
                    Name = order.User.Name,
                    Email = order.User.Email
                },
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Product = new ProductDto
                    {
                        Id = oi.Product.Id,
                        Name = oi.Product.Name,
                        Price = oi.Product.Price,
                        Description = oi.Product.Description
                    }
                }).ToList()
            };
        }






        //public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        //{
        //    return await _orderRepository.GetAllOrdersAsync();
        //}

        //public async Task<OrderDto> GetOrderByIdAsync(int id)
        //{
        //    var order = await _orderRepository.GetOrderByIdAsync(id);

        //    if (order == null) return null;

        //    // Map to OrderDto
        //    var orderDto = new OrderDto
        //    {
        //        Id = order.Id,
        //        UserId = order.UserId,
        //        OrderDate = order.OrderDate,
        //        TotalAmount = order.TotalAmount,
        //        Status = order.Status,
        //        User = new UserDto
        //        {
        //            Id = order.User.Id,
        //            Name = order.User.Name,
        //            Email = order.User.Email
        //        },
        //        OrderItems = order.OrderItems.Select(oi => new OrderItemDto
        //        {
        //            Id = oi.Id,
        //            OrderId = oi.OrderId,
        //            ProductId = oi.ProductId,
        //            Quantity = oi.Quantity,
        //            UnitPrice = oi.UnitPrice,
        //            Product = new ProductDto
        //            {
        //                Id = oi.Product.Id,
        //                Name = oi.Product.Name,
        //                Price = oi.Product.Price,
        //                Description = oi.Product.Description
        //            }
        //        }).ToList()
        //    };

        //    return orderDto;
        //}


        //public async Task<OrderDto> GetOrderByIdAsync(int id)
        //{
        //    var order = await _orderRepository.GetOrderByIdAsync(id);

        //    if (order == null) return null;

        //    // Ab OrderDto me map kar rahe hain (yahan pe .Include ki zarurat nahi hai kyunki OrderRepository me already ho chuka hai)
        //    var orderDto = new OrderDto
        //    {
        //        Id = order.Id,
        //        UserId = order.UserId,
        //        OrderDate = order.OrderDate,
        //        TotalAmount = order.TotalAmount,
        //        Status = order.Status,
        //        User = new UserDto
        //        {
        //            Id = order.User.Id,
        //            Name = order.User.Name,
        //            Email = order.User.Email
        //        },
        //        OrderItems = order.OrderItems.Select(oi => new OrderItemDto
        //        {
        //            Id = oi.Id,
        //            OrderId = oi.OrderId,
        //            ProductId = oi.ProductId,
        //            Quantity = oi.Quantity,
        //            UnitPrice = oi.UnitPrice,
        //            Product = new ProductDto
        //            {
        //                Id = oi.Product.Id,
        //                Name = oi.Product.Name,
        //                Price = oi.Product.Price,
        //                Description = oi.Product.Description
        //            }
        //        }).ToList()
        //    };

        //    return orderDto;
        //}


        public async Task<OrderDto> CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrderAsync(order);

            // Fetch the created order with related data
            var createdOrder = await _orderRepository.GetOrderByIdAsync(order.Id);

            // Map the created order to OrderDto
            var orderDto = new OrderDto
            {
                Id = createdOrder.Id,
                UserId = createdOrder.UserId,
                OrderDate = createdOrder.OrderDate,
                TotalAmount = createdOrder.TotalAmount,
                Status = createdOrder.Status,
                User = new UserDto
                {
                    Id = createdOrder.User.Id,
                    Name = createdOrder.User.Name,
                    Email = createdOrder.User.Email,
                    Role = createdOrder.User.Role ?? "DefaultRole",  // Provide a default if null
                    Token = createdOrder.User.Token ?? "DefaultToken"  // Provide a default if null
                },
                OrderItems = createdOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Product = new ProductDto
                    {
                        Id = oi.Product.Id,
                        Name = oi.Product.Name,
                        Price = oi.Product.Price,
                        Description = oi.Product.Description
                    }
                }).ToList()
            };


            return orderDto;
        }



        //public async Task CreateOrderAsync(Order order)
        //{
        //    await _orderRepository.CreateOrderAsync(order);
        //}

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
