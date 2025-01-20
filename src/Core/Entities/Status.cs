namespace Core.Entities
{
    public class Status
    {
        public int  Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}