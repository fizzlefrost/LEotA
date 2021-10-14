using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class CalabongaGetPagedRequestModel
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? SortDirection { get; set; }
        public string? Search { get; set; }
        [JsonPropertyName("disabledDefaultIncludes")]
        public bool DisabledDefaultIncludes { get; set; }
    }
}