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
        [JsonPropertyName("author")]
        public CultureBase? Author { get; set; }
        [JsonPropertyName("fileType")]
        public FileType FileType { get; set; }
    }
    
    public enum FileType
    {
        Image, Publication, MiniImage, AlbumMainImage
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
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("fileType")]
        public int FileType { get; set; }
    }
    
    public class FileContentCreateModel
    {
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("masterId")]
        public string MasterId { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("fileType")]
        public int FileType { get; set; }
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
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("fileType")]
        public int FileType { get; set; }
    }
}