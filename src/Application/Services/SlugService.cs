using Core.Interfaces;

namespace Application.Common
{
    public class SlugService : ISlugService
    {
        /// <summary>
        /// Generates a unique slug based on the provided title.
        /// </summary>
        /// <param name="title">The title to generate the slug from.</param>
        /// <param name="repository">The repository used to check for slug existence.</param>
        /// <returns>
        /// A unique slug string. If the initial slug already exists, a timestamp is appended to ensure uniqueness.
        /// </returns>
        public async Task<string> GenerateUniqueSlugAsync(string title, ISlugRepository repository)
        {
            var slug = ContentHelper.GenerateSlug(title);

            if (await repository.SlugExistsAsync(slug))
            {
                var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                slug = $"{slug}-{timestamp}";
            }

            return slug;
        }

    }
}
