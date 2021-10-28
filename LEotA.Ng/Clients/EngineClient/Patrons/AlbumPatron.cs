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
    public class AlbumPatron : IAlbumPatron
    {
        private HttpClient _httpClient;

        public AlbumPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<Album>> AlbumGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/album/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(result, options);
            return AlbumGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Album>> AlbumPostAsync(AlbumCreateModel AlbumCreateModel)
        {
            var request = new AlbumCreateModel()
            {
                Name = AlbumCreateModel.Name
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/album/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return AlbumGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Album>> AlbumGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/album/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(result, options);
            return AlbumGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Album>> AlbumPutAsync(AlbumUpdateModel AlbumUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/album/post-item", AlbumUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return AlbumGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Album>> AlbumDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/album/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(result, options);
            return AlbumGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Album>> AlbumGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/album/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<AlbumGetModel>>(result, options);
            return AlbumGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaGetPagedModel<Album>> AlbumGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/album/get-paged");
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
            var pageModel = JsonSerializer.Deserialize<CalabongaGetPagedModel<AlbumGetModel>>(result, options);
            return AlbumGetPagedModelToAboutUs(pageModel);
        }
        
        private CalabongaViewModel<Album> AlbumGetModelToAboutUs(CalabongaViewModel<AlbumGetModel> pageModel)
        {
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var returnModel = new CalabongaViewModel<Album>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Album()
                {
                    Id = new Guid(pageModel.Result.Id),
                    Name = culturedName
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Album> AlbumGetPagedModelToAboutUs(CalabongaGetPagedModel<AlbumGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Album>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Album>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Album>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var album in pageModel.Result.Items)
                {
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(album.Name);
                    returnModel.Result.Items.Add(new Album()
                    {
                        Id = new Guid(album.Id),
                        Name = culturedName
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Album>();
            }
        }
    }
}