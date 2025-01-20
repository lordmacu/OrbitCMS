namespace Core.Entities
{
    public class User
    {
        public Guid  Id { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public Guid? RolId { get; set; }
        public Rol? Rol { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}