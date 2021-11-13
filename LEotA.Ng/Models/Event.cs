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
        public CultureBase Name { get; set; }
        [JsonPropertyName("text")]
        public CultureBase Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string? EmbedLink { get; set; }
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }
    }
    
    public class EventGetModel  
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        
        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }
    }
    
    public class EventCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        
        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }
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
        
        [JsonPropertyName("dateTime")]
        public string DateTime { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}