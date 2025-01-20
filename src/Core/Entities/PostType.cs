namespace Core.Entities
{
    public class PostType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
