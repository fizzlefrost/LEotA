using System;
using System.Collections.Generic;
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
    public class AboutUsPatron : IAboutUsPatron
    {
        private HttpClient _httpClient;

        public AboutUsPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(result, options);
        }

        public async Task<CalabongaViewModel<AboutUs>> AboutUsPostAsync(AboutUsCreateModel aboutUsCreateModel)
        {
            var request = new AboutUsCreateModel()
            {
                Text = JsonSerializer.Serialize(aboutUsCreateModel.Text), 
                Image = aboutUsCreateModel.Image
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<AboutUs>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }

        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(result, options);
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsPutAsync(AboutUsUpdateModel aboutUsUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", aboutUsUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<AboutUs>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/about-us/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<CalabongaViewModel<AboutUs>>(result, options);
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
            var pageModel = JsonSerializer.Deserialize<CalabongaGetPagedModel<AboutUsGetModel>>(result, options);

            var returnModel = new CalabongaGetPagedModel<AboutUs>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Page<AboutUs>()
                {
                    HasNextPage = pageModel.Result.HasNextPage,
                    HasPreviousPage = pageModel.Result.HasPreviousPage,
                    IndexFrom = pageModel.Result.IndexFrom,
                    Items = new List<AboutUs>(),
                    PageIndex = pageModel.Result.PageIndex,
                    PageSize = pageModel.Result.PageSize,
                    TotalCount = pageModel.Result.TotalCount,
                    TotalPages = pageModel.Result.TotalPages
                }
            };
            foreach (var aboutUs in pageModel.Result.Items)
            {
                var culturedText = JsonSerializer.Deserialize<CultureBase>(aboutUs.Text);
                returnModel.Result.Items.Add(new AboutUs()
                {
                    Id = new Guid(aboutUs.Id),
                    Image = Convert.FromBase64String(aboutUs.Image),
                    Text = culturedText
                });
            }
            return returnModel;
        }
    }
}