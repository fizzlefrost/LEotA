using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Album
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public CultureBase Name { get; set; }
    }
    
    public class AlbumGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    
    public class AlbumCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    
    public class AlbumUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}