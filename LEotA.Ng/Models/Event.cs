using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Event    
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
    }
    
    public class EventCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
    }
    
    public class EventViewModel
    {
        public Event Event { get; set; }
        public List<Image>? Image { get; set; }
    }
    
    public class EventUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("culture")]
        public string Culture { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}