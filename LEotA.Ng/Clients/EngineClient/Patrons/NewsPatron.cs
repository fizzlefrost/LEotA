using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public class NewsPatron : INewsPatron
    {
        private HttpClient _httpClient;

        public NewsPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<News>> NewsGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<News>>(result, options);
        }

        public async Task<CalabongaViewModel<News>> NewsPostAsync(NewsCreateModel NewsCreateModel)
        {
            var request = new NewsCreateModel()
            {
                Description = NewsCreateModel.Description,
                NewsColumnId = NewsCreateModel.NewsColumnId,
                Culture = NewsCreateModel.Culture
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<News>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<News>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }

        public async Task<CalabongaViewModel<News>> NewsGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<News>>(result, options);
        }
        
        public async Task<CalabongaViewModel<News>> NewsPutAsync(NewsUpdateModel NewsUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", NewsUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<News>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<News>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<News>> NewsDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/about-us/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<News>>(result, options);
        }

        public async Task<CalabongaViewModel<News>> NewsGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<News>>(result, options);
        }
        
        public async Task<CalabongaGetPagedModel<News>> NewsGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/about-us/get-paged");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["PageIndex"] = parameters.PageIndex.ToString();
            query["PageSize"] = parameters.PageSize.ToString();
            query["SortDirection"] = parameters.SortDirection.ToString();
            query["Search"] = parameters.Search;
            query["disabledDefaultIncludes"] = parameters.DisabledDefaultIncludes.ToString();
            builder.Query = query.ToString() ?? string.Empty;
            string url = builder.ToString();
            var httpResponse = await _httpClient.GetAsync(url);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaGetPagedModel<News>>(result, options);
        }
    }
}