﻿using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Image
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("imageRaw")]
        public byte[] ImageRaw { get; set; }
        [JsonPropertyName("masterId")]
        public Guid MasterId { get; set; }
    }
    
    public class ImageCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("imageRaw")]
        public byte[] ImageRaw { get; set; }
        [JsonPropertyName("masterId")]
        public Guid MasterId { get; set; }
    }
    
    public class ImageUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("imageRaw")]
        public byte[] ImageRaw { get; set; }
        [JsonPropertyName("masterId")]
        public Guid MasterId { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}