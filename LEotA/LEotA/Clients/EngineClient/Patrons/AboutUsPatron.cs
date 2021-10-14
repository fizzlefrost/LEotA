using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using LEotA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Clients.EngineClient
{
    public class AboutUsPatron : IAboutUsPatron
    {
        private HttpClient _httpClient;

        public AboutUsPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModelForCreation> AboutUsGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModelForCreation>(result, options);
        }

        public async Task<string> AboutUsPostAsync(string text, string imageRaw)
        {
            var request = new AboutUsCreateModel()
            {
                Text = text, 
                Image = imageRaw
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            string report = new string("");
            try
            {
                report = JsonSerializer.Deserialize<string>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModelForEditing<AboutUs>> AboutUsGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModelForEditing<AboutUs>>(result, options);
        }
        
        public async Task<string> AboutUsPutAsync(AboutUsUpdateModel aboutUsUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", aboutUsUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            string report = new string("");
            try
            {
                report = JsonSerializer.Deserialize<string>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(result, options);
        }
        
        public async Task<CalabongaGetPagedModel<AboutUs>> AboutUsGetPagedAsync(CalabongaGetPagedRequestModel parameters)
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
            return JsonSerializer.Deserialize<CalabongaGetPagedModel<AboutUs>>(result, options);
        }
    }
}