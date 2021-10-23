using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("newsColumnId")]
        public Guid NewsColumnId { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
    }
    
    public class NewsCreateModel
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("newsColumnId")]
        public Guid NewsColumnId { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
    }
    
    public class NewsUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("newsColumnId")]
        public Guid NewsColumnId { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
    }
}