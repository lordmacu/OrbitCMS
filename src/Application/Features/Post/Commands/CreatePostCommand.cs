using System.Text.Json.Serialization;

namespace Application.Features.Posts.Commands
{
    public class CreatePostCommand
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public Guid? PostType { get; set; }
        public List<string> Categories { get; set; } = new List<string>(); // Puede contener GUIDs o nombres

        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; }

        [JsonPropertyName("status_id")]
        public Guid? StatusId { get; set; }
        
    }
}
