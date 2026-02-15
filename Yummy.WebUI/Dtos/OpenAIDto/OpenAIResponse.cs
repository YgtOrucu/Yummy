using System.Text.Json.Serialization;

namespace Yummy.WebUI.Dtos.OpenAIDto
{
    public class OpenAIResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choice { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
