using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class News
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("description")]
        public CultureBase Description { get; set; }
        [JsonPropertyName("name")]
        public CultureBase Name { get; set; }
        [JsonPropertyName("text")]
        public CultureBase Text { get; set; }
        [JsonPropertyName("author")]
        public CultureBase? Author { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }

    public class NewsGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }

    public class NewsCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
    
    public class NewsUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
}