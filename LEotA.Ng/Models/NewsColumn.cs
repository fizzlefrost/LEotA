using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class NewsColumn
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
    
    public class NewsColumnCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
    public class NewsViewModel
    {
        public Event Event { get; set; }
        public List<Image>? Image { get; set; }
    }
    public class NewsColumnUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}