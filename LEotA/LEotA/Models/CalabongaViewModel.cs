using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class CalabongaViewModel<T>
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }
        
        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("activityId")]
        public string ActivityId { get; set; }

        [JsonPropertyName("metadata")]
        public object Metadata { get; set; }

        [JsonPropertyName("exception")]
        public object Exception { get; set; }

        [JsonPropertyName("logs")]
        public List<object> Logs { get; set; }
    }
}