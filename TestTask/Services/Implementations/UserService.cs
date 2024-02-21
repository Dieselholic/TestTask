using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetUser()
        {
            var topUser = _context
                .Users
                .Include(x => x.Orders)
                .OrderByDescending(x => x.Orders.Count)
                .AsNoTracking()
                .First();


            return _context
                .Users
                .OrderByDescending(x => x.Orders.Count)
                .AsNoTracking()
                .FirstAsync();
        }

        public Task<List<User>> GetUsers()
        {
            return _context
                .Users
                .Where(u => u.Status == UserStatus.Inactive)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
