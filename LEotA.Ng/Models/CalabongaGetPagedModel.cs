using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class CalabongaGetPagedModel<T>
    {
        [JsonPropertyName("result")]
        public Page<T> Result { get; set; }

        [JsonPropertyName("ok")]
        public bool Ok { get; set; }

        [JsonPropertyName("activityId")]
        public string ActivityId { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("exception")]
        public object Exception { get; set; }

        [JsonPropertyName("logs")]
        public List<string> Logs { get; set; }
    }
    public class Page<T>
    {
        [JsonPropertyName("indexFrom")]
        public int IndexFrom { get; set; }

        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("items")]
        public List<T> Items { get; set; }

        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("dataObject")]
        public object DataObject { get; set; }
    }

    


}