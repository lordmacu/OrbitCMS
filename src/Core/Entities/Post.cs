namespace Core.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public PostType? PostType { get; set; }
        public Guid? PostTypeId { get; set; }
        public Guid? StatusId {get; set;}
        public Status? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}