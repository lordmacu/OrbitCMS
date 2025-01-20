namespace Core.Interfaces
{
    public interface IStatusRepository
    {
        Task<Guid?> GetIdByNameAsync(string name);
    }
}
