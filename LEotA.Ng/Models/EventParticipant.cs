using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class EventParticipant
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
        [JsonPropertyName("eventId")]
        public Guid EventId { get; set; }
    }
    
    public class EventParticipantGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }
    }
    
    public class EventParticipantCreateModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }
    }
    
    public class EventParticipantUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}