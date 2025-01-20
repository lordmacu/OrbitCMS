using System.Text.Json.Serialization;

namespace Application.Features.Posts.Commands
{
    public class CreatePostCommand
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }
    }
}
