namespace Core.Entities
{
    public class Rol
    {
        public Guid  Id { get; set; }
        public string? Name { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}