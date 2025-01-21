using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for user-related operations.</param>
        /// <remarks>
        /// This constructor injects the database context, allowing the repository to interact with the user data in the database.
        /// </remarks>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Asynchronously retrieves the ID of a user based on their name.
        /// </summary>
        /// <param name="name">The name of the user to search for.</param>
        /// <returns>
        /// A nullable Guid representing the user's ID if found; otherwise, null.
        /// </returns>
        public async Task<Guid?> GetIdByNameAsync(string name)
        { 
            return await _context.Users
                .Where(s => s.Name == name)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }


    }
}
