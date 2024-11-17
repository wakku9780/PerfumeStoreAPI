using System;
using System.Collections.Generic;

namespace PerfumeStoreAPI.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        // UserDto property ko add kar rahe hain
        public UserDto? User { get; set; }
    }
}
