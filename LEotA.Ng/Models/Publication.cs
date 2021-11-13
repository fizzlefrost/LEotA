using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Publication
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("text")]
        public CultureBase Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("name")]
        public CultureBase Name { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
    
    public class PublicationGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
    
    public class PublicationCreateModel
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
    
    public class PublicationUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
}