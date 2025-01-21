using Core.Interfaces;

namespace Application.Common
{
    public class SlugService : ISlugService
    {
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
