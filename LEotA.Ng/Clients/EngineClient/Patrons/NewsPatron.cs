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
    public class NewsPatron : INewsPatron
    {
        private HttpClient _httpClient;

        public NewsPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CalabongaViewModel<News>> NewsPostAsync(NewsCreateModel NewsCreateModel)
        {
            var request = new NewsCreateModel()
            {
                Description = NewsCreateModel.Description,
                Name = NewsCreateModel.Name,
                Text = NewsCreateModel.Text
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/news/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<NewsGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return NewsGetModelToNews(report);
        }

        public async Task<CalabongaViewModel<News>> NewsPutAsync(NewsUpdateModel NewsUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/news/post-item", NewsUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<NewsGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return NewsGetModelToNews(report);
        }
        
        public async Task<CalabongaViewModel<News>> NewsDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/news/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<NewsGetModel>>(result, options);
            return NewsGetModelToNews(report);
        }

        public async Task<CalabongaViewModel<News>> NewsGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/news/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<NewsGetModel>>(result, options);
            return NewsGetModelToNews(report);
        }

        public async Task<int> NewsGetTotalPages(int? pageSize)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/news/total-pages");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var report = JsonSerializer.Deserialize<int>(result);
            return report;
        }
        public async Task<CalabongaGetPagedModel<News>> NewsGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/news/get-paged");
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<NewsGetModel>>(result, options);
            return NewsGetPagedModelToNews(report);
        }
        
        private CalabongaViewModel<News> NewsGetModelToNews(CalabongaViewModel<NewsGetModel> pageModel)
        {
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var culturedDescription = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Description);
            var returnModel = new CalabongaViewModel<News>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new News()
                {
                    Id = new Guid(pageModel.Result.Id),
                    Name = culturedName,
                    Text = culturedText,
                    Description = culturedDescription
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<News> NewsGetPagedModelToNews(CalabongaGetPagedModel<NewsGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<News>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<News>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<News>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var News in pageModel.Result.Items)
                {
                    var culturedText = JsonSerializer.Deserialize<CultureBase>(News.Text);
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(News.Name);
                    var culturedDescription = JsonSerializer.Deserialize<CultureBase>(News.Description);
                    returnModel.Result.Items.Add(new News()
                    {
                        Id = new Guid(News.Id),
                        Text = culturedText,
                        Description = culturedDescription,
                        Name = culturedName
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<News>();
            }
        }
    }
}