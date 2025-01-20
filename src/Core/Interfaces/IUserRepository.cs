namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid?> GetIdByNameAsync(string name);
    }
}
