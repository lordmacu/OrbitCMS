namespace Core.Interfaces
{
    public interface IPostTypeRepository
    {
        Task<Guid?> GetIdByNameAsync(string name);
    }
}