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
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(result, options);
            return ImageGetModelToAboutUs(report);
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
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return ImageGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Image>> ImageGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/image/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(result, options);
            return ImageGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Image>> ImagePutAsync(ImageUpdateModel ImageUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/image/post-item", ImageUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return ImageGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Image>> ImageDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/image/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(result, options);
            return ImageGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Image>> ImageGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/image/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ImageGetModel>>(result, options);
            return ImageGetModelToAboutUs(report);
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<ImageGetModel>>(result, options);
            return ImageGetPagedModelToAboutUs(report);
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<ImageGetModel>>(result, options);
            return ImageGetPagedModelToAboutUs(report);
        }
        
        private CalabongaViewModel<Image> ImageGetModelToAboutUs(CalabongaViewModel<ImageGetModel> pageModel)
        {
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var returnModel = new CalabongaViewModel<Image>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Image()
                {
                    Id = new Guid(pageModel.Result.Id),
                    Name = culturedName,
                    ImageRaw = Convert.FromBase64String(pageModel.Result.ImageRaw),
                    MasterId = new Guid(pageModel.Result.MasterId)
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Image> ImageGetPagedModelToAboutUs(CalabongaGetPagedModel<ImageGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Image>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Image>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Image>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var Image in pageModel.Result.Items)
                {
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(Image.Name);
                    returnModel.Result.Items.Add(new Image()
                    {
                        Id = new Guid(Image.Id),
                        Name = culturedName,
                        ImageRaw = Convert.FromBase64String(Image.ImageRaw),
                        MasterId = new Guid(Image.MasterId)
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Image>();
            }
        }
    }
}