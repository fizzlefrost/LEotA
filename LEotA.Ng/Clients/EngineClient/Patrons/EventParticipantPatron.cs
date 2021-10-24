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
    public class EventParticipantPatron : IEventParticipantPatron
    {
        private HttpClient _httpClient;

        public EventParticipantPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantGetViewModelForCreationAsync()
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-creation");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(result, options);
            return EventParticipantGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantPostAsync(EventParticipantCreateModel EventParticipantCreateModel)
        {
            var request = new EventParticipantCreateModel()
            {
                EventId = EventParticipantCreateModel.EventId,
                UserId = EventParticipantCreateModel.UserId
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/about-us/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return EventParticipantGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantGetViewModelForEditingAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-viewmodel-for-editing/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(result, options);
            return EventParticipantGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantPutAsync(EventParticipantUpdateModel EventParticipantUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/about-us/post-item", EventParticipantUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return EventParticipantGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/about-us/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(result, options);
            return EventParticipantGetModelToAboutUs(report);
        }

        public async Task<CalabongaViewModel<EventParticipant>> EventParticipantGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/about-us/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<EventParticipantGetModel>>(result, options);
            return EventParticipantGetModelToAboutUs(report);
        }
        
        public async Task<CalabongaGetPagedModel<EventParticipant>> EventParticipantGetPagedAsync(CalabongaGetPagedRequestModel parameters)
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<EventParticipantGetModel>>(result, options);
            return EventParticipantGetPagedModelToAboutUs(report);
        }
        
        private CalabongaViewModel<EventParticipant> EventParticipantGetModelToAboutUs(CalabongaViewModel<EventParticipantGetModel> pageModel)
        {
            var returnModel = new CalabongaViewModel<EventParticipant>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new EventParticipant()
                {
                    Id = new Guid(pageModel.Result.Id),
                    EventId = new Guid(pageModel.Result.EventId),
                    UserId = new Guid(pageModel.Result.UserId)
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<EventParticipant> EventParticipantGetPagedModelToAboutUs(CalabongaGetPagedModel<EventParticipantGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<EventParticipant>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<EventParticipant>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<EventParticipant>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var eventParticipant in pageModel.Result.Items)
                {
                    returnModel.Result.Items.Add(new EventParticipant()
                    {
                        Id = new Guid(eventParticipant.Id),
                        EventId = new Guid(eventParticipant.EventId),
                        UserId = new Guid(eventParticipant.UserId)
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<EventParticipant>();
            }
        }
    }
}