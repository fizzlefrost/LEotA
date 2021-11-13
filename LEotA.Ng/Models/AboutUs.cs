using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class AboutUs
    {
        [JsonPropertyName("text")]
        public CultureBase Text { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
    
    public class AboutUsGetModel
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
    
    public class AboutUsCreateModel
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
    
    public class AboutUsUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("Text")]
        public string Text { get; set; }
        [JsonPropertyName("Id")]
        public string NewId { get; set; }
    }
}