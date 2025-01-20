using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostTypeRepository : IPostTypeRepository
    {
        private readonly AppDbContext _context;

        public PostTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            return await _context.PostTypes
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

    }
}
