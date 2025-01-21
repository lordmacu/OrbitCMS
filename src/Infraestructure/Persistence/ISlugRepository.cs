namespace Core.Interfaces
{
    public interface ISlugRepository
    {
        Task<bool> SlugExistsAsync(string slug);
    }
}
