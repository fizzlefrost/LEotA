using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class CultureBase
    {
        [JsonPropertyName("en")]
        public string English { get; set; }
        [JsonPropertyName("ru")]
        public string Russian { get; set; }
    }
}