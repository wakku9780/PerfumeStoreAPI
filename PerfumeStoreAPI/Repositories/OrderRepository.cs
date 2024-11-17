using Microsoft.EntityFrameworkCore;
using PerfumeStoreAPI.Data;
using PerfumeStoreAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PerfumeStoreContext _context;

        public OrderRepository(PerfumeStoreContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }


        //public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        //{
        //    return await _context.Orders
        //        .Include(o => o.User)
        //        .Include(o => o.OrderItems)
        //            .ThenInclude(oi => oi.Product)
        //        .ToListAsync();
        //}

        //public async Task<Order> GetOrderByIdAsync(int id)
        //{
        //    return await _context.Orders
        //        .Include(o => o.User)
        //        .Include(o => o.OrderItems)
        //            .ThenInclude(oi => oi.Product)
        //        .FirstOrDefaultAsync(o => o.Id == id);
        //}

        public async Task CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
