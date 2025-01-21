using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostTypeRepository : IPostTypeRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostTypeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data access operations.</param>
        /// <remarks>
        /// This constructor sets up the repository with the provided database context,
        /// allowing it to perform database operations related to post types.
        /// </remarks>
        public PostTypeRepository(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Asynchronously retrieves the ID of a post type based on its name.
        /// </summary>
        /// <param name="name">The name of the post type to search for.</param>
        /// <returns>
        /// A nullable Guid representing the ID of the post type if found;
        /// otherwise, null if no matching post type is found.
        /// </returns>
        public async Task<Guid?> GetIdByNameAsync(string name)
        {
            return await _context.PostTypes
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

    }
}
