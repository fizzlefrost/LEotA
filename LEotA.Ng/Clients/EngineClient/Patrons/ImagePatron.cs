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
    public class ImagePatron : IImagePatron
    {
        private HttpClient _httpClient;

        public ImagePatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<Image>> ImageGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/image/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<Image>>(result, options);
        }

        public async Task<CalabongaViewModel<Image>> ImagePostAsync(ImageCreateModel ImageCreateModel)
        {
            var request = new ImageCreateModel()
            {
                ImageRaw = ImageCreateModel.ImageRaw,
                MasterId = ImageCreateModel.MasterId,
                Name = ImageCreateModel.Name
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/image/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<Image>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<Image>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }

        public async Task<CalabongaViewModel<Image>> ImageGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/image/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<Image>>(result, options);
        }
        
        public async Task<CalabongaViewModel<Image>> ImagePutAsync(ImageUpdateModel ImageUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/image/post-item", ImageUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<Image>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<Image>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<Image>> ImageDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/image/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<Image>>(result, options);
        }

        public async Task<CalabongaViewModel<Image>> ImageGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/image/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<Image>>(result, options);
        }
        
        public async Task<CalabongaGetPagedModel<Image>> ImageGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/image/get-paged");
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
            return JsonSerializer.Deserialize<CalabongaGetPagedModel<Image>>(result, options);
        }

        public async Task<CalabongaGetPagedModel<Image>> ImageGetByMasterIdAsync(string id)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/image/get-image-by-masterId");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["PageSize"] = 100.ToString();
            builder.Query = query.ToString() ?? string.Empty;
            string url = builder.ToString();
            var httpResponse = await _httpClient.GetAsync(url);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaGetPagedModel<Image>>(result, options);
        }
    }
}