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
        private readonly HttpClient _httpClient;

        public AboutUsPatron(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Engine");
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(result, options);
            return AboutUsGetModelToAboutUs(report);
            
        }

        public async Task<CalabongaViewModel<AboutUs>> AboutUsPostAsync(AboutUsCreateModel aboutUsCreateModel)
        {
            var request = new AboutUsCreateModel()
            {
                Text = JsonSerializer.Serialize(aboutUsCreateModel.Text)
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(json, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return AboutUsGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(result, options);
            return AboutUsGetModelToAboutUs(pageModel);
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsPutAsync(AboutUsUpdateModel aboutUsUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", aboutUsUpdateModel);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{result}");
            var report = new CalabongaViewModel<AboutUs>();
            try
            {
                var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(result, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                return AboutUsGetModelToAboutUs(pageModel);
            }
            catch (Exception exception)
            {
                // throw new JsonException($"Ошибка десериализации {Environment.NewLine}{aboutUsUpdateModel.Id}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<AboutUs>> AboutUsDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/about-us/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(result, options);
            return AboutUsGetModelToAboutUs(pageModel);
        }

        public async Task<CalabongaViewModel<AboutUs>> AboutUsGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<AboutUsGetModel>>(result, options);
            return AboutUsGetModelToAboutUs(pageModel);
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
            return AboutUsGetPagedModelToAboutUs(pageModel);
        }
        
        private CalabongaViewModel<AboutUs> AboutUsGetModelToAboutUs(CalabongaViewModel<AboutUsGetModel> pageModel)
        {
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var returnModel = new CalabongaViewModel<AboutUs>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new AboutUs()
                {
                    Id = new Guid(pageModel.Result.Id),
                    Text = culturedText
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<AboutUs> AboutUsGetPagedModelToAboutUs(CalabongaGetPagedModel<AboutUsGetModel> pageModel)
        {
            try
            {
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
                        Text = culturedText
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<AboutUs>();
            }
        }
    }
}