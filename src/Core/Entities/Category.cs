namespace Core.Entities
{
    public class Category
    {
        public Guid  Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}