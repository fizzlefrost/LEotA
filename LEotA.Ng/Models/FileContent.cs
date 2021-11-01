using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class FileContent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
        [JsonPropertyName("content")]
        public byte[] Content { get; set; }
        [JsonPropertyName("masterId")]
        public Guid MasterId { get; set; }
    }
    
    public class FileContentGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("masterId")]
        public string MasterId { get; set; }
    }
    
    public class FileContentCreateModel
    {
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("masterId")]
        public string MasterId { get; set; }
    }
    
    public class FileContentUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("masterId")]
        public string MasterId { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}