public class PostDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Excerpt { get; set; }
    public string? Slug { get; set; }
    public AuthorDto? Author { get; set; }
    public List<CategoryDto>? Categories { get; set; }
    public PostTypeDto? PostType { get; set; }
    public StatusDto? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}