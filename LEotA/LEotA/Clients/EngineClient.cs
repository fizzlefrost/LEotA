using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LEotA.Clients
{
    public class EngineClient
    {
        private static HttpClient _client;

        public EngineClient(HttpClient client)
        {
            _client = client;
        }

        public static async Task<string> PostAboutUs(string text, string imageRaw)
        {
            const string url = "/api/about-us/post-item";

            var request = new PostAboutUsRequest
            {
                Text = text, 
                Image = imageRaw
            };
            var requestJson = JsonSerializer.Serialize(request);

            var content = new StringContent(
                requestJson,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync(url, content);

            var json = await response.Content.ReadAsStringAsync();

            var model = JsonSerializer.Deserialize<PostAboutUsResponseModel>(json);

            return model.Result.Id;
        }

        private class PostAboutUsRequest
        {
            public string Text { get; set; }
            public string Image { get; set; }
        }
        
        public class Result
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("image")]
            public string Image { get; set; }

            [JsonPropertyName("id")]
            public string Id { get; set; }
        }

        public class PostAboutUsResponseModel
        {
            [JsonPropertyName("result")]
            public Result Result { get; set; }

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
}