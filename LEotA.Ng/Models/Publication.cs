﻿using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Publication
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("pdfRaw")]
        public byte[] PDFRaw { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
    }
    
    public class PublicationCreateModel
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("pdfRaw")]
        public byte[] PDFRaw { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
    }
    
    public class PublicationUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("pdfRaw")]
        public byte[] PDFRaw { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}