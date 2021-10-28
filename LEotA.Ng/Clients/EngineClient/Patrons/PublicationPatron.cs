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
    public class PublicationPatron : IPublicationPatron
    {
        private HttpClient _httpClient;

        public PublicationPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<Publication>> PublicationGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/publication/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(result, options);
            return PublicationGetModelToPublication(report);
        }

        public async Task<CalabongaViewModel<Publication>> PublicationPostAsync(PublicationCreateModel PublicationCreateModel)
        {
            var request = new PublicationCreateModel()
            {
                EmbedLink = PublicationCreateModel.EmbedLink,
                PDFRaw = PublicationCreateModel.PDFRaw,
                Text = PublicationCreateModel.Text
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/publication/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return PublicationGetModelToPublication(report);
        }

        public async Task<CalabongaViewModel<Publication>> PublicationGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/publication/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(result, options);
            return PublicationGetModelToPublication(report);
        }
        
        public async Task<CalabongaViewModel<Publication>> PublicationPutAsync(PublicationUpdateModel PublicationUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/publication/post-item", PublicationUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return PublicationGetModelToPublication(report);
        }
        
        public async Task<CalabongaViewModel<Publication>> PublicationDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/publication/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(result, options);
            return PublicationGetModelToPublication(report);
        }

        public async Task<CalabongaViewModel<Publication>> PublicationGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/publication/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<PublicationGetModel>>(result, options);
            return PublicationGetModelToPublication(report);
        }
        
        public async Task<CalabongaGetPagedModel<Publication>> PublicationGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/publication/get-paged");
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<PublicationGetModel>>(result, options);
            return PublicationGetPagedModelToPublication(report);
        }
        
        private CalabongaViewModel<Publication> PublicationGetModelToPublication(CalabongaViewModel<PublicationGetModel> pageModel)
        {
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var returnModel = new CalabongaViewModel<Publication>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Publication()
                {
                    Id = new Guid(pageModel.Result.Id),
                    Text = culturedText,
                    EmbedLink = pageModel.Result.EmbedLink,
                    PDFRaw = Convert.FromBase64String(pageModel.Result.PDFRaw)
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Publication> PublicationGetPagedModelToPublication(CalabongaGetPagedModel<PublicationGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Publication>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Publication>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Publication>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var Publication in pageModel.Result.Items)
                {
                    var culturedText = JsonSerializer.Deserialize<CultureBase>(Publication.Text);
                    returnModel.Result.Items.Add(new Publication()
                    {
                        Id = new Guid(Publication.Id),
                        Text = culturedText,
                        EmbedLink = Publication.EmbedLink,
                        PDFRaw = Convert.FromBase64String(Publication.PDFRaw)
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Publication>();
            }
        }
    }
}