using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access operations.</param>
        /// <remarks>
        /// This constructor injects the database context dependency required for the repository's operations.
        /// </remarks>
        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Asynchronously retrieves the ID of a status by its name.
        /// </summary>
        /// <param name="name">The name of the status to search for.</param>
        /// <returns>
        /// A nullable Guid representing the ID of the status if found; otherwise, null.
        /// </returns>
        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            return await _context.Statuses
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }


    }
}
