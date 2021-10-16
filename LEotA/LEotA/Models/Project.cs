using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Project
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
    }
    
    public class ProjectCreateModel
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
    }
    
    public class ProjectUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}