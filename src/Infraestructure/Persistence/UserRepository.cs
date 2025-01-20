using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetIdByNameAsync(string name)
        { 
            return await _context.Users
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

    }
}
