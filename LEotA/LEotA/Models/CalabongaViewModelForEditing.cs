using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class CalabongaViewModelForEditing<T> : CalabongaViewModel<T>
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }
        
    }
}