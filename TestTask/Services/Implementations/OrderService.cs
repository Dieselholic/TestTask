using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order> GetOrder()
        {
            return _context
                .Orders
                .OrderByDescending(x => x.Price * x.Quantity)
                .AsNoTracking()
                .FirstAsync();
        }

        public Task<List<Order>> GetOrders()
        {
            return _context
                .Orders
                .Where(x => x.Quantity > 10)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
