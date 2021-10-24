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
    public class EventPatron : IEventPatron
    {
        private HttpClient _httpClient;

        public EventPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<Event>> EventGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(result, options);
            return EventGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Event>> EventPostAsync(EventCreateModel EventCreateModel)
        {
            var request = new EventCreateModel()
            {
                EmbedLink = EventCreateModel.EmbedLink,
                Name = EventCreateModel.Name,
                Text = EventCreateModel.Text
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return EventGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Event>> EventGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(result, options);
            return EventGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Event>> EventPutAsync(EventUpdateModel EventUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", EventUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return EventGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<Event>> EventDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/about-us/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(result, options);
            return EventGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<Event>> EventGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventGetModel>>(result, options);
            return EventGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaGetPagedModel<Event>> EventGetPagedAsync(CalabongaGetPagedRequestModel parameters)
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<EventGetModel>>(result, options);
            return EventGetPagedModelToAboutUs(report);
        }
        
        private CalabongaViewModel<Event> EventGetModelToAboutUs(CalabongaViewModel<EventGetModel> pageModel)
        {
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var returnModel = new CalabongaViewModel<Event>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Event()
                {
                    Id = new Guid(pageModel.Result.Id),
                    EmbedLink = pageModel.Result.EmbedLink,
                    Name = culturedName,
                    Text = culturedText
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Event> EventGetPagedModelToAboutUs(CalabongaGetPagedModel<EventGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Event>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Event>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Event>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var Event in pageModel.Result.Items)
                {
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(Event.Name);
                    var culturedText = JsonSerializer.Deserialize<CultureBase>(Event.Text);
                    returnModel.Result.Items.Add(new Event()
                    {
                        Id = new Guid(Event.Id),
                        EmbedLink = Event.EmbedLink,
                        Name = culturedName,
                        Text = culturedText
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Event>();
            }
        }
    }
}