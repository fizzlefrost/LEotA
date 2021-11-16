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
using StaffRoles = LEotA.Engine.Entities.StaffRoles;

namespace LEotA.Clients.EngineClient.Patrons
{
    public class StaffPatron : IStaffPatron
    {
        private HttpClient _httpClient;

        public StaffPatron(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<CalabongaViewModel<Staff>> StaffPostAsync(StaffCreateModel staffCreateModel)
        {
            var request = new StaffCreateModel()
            {
                EmbedLink = staffCreateModel.EmbedLink,
                Name = staffCreateModel.Name,
                Role = staffCreateModel.Role,
                Email = staffCreateModel.Email,
                Text = staffCreateModel.Text
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/staff/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<StaffGetModel>>(json, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return StaffGetModelToStaff(report);
        }

        public async Task<CalabongaViewModel<Staff>> StaffPutAsync(StaffUpdateModel staffUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/staff/post-item", staffUpdateModel);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{result}");
            var report = new CalabongaViewModel<Staff>();
            try
            {
                var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<StaffGetModel>>(result, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                return StaffGetModelToStaff(pageModel);
            }
            catch (Exception exception)
            {
                // throw new JsonException($"Ошибка десериализации {Environment.NewLine}{StaffUpdateModel.Id}", exception);
            }
            return report;
        }
        
        public async Task<CalabongaViewModel<Staff>> StaffDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/staff/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<StaffGetModel>>(result, options);
            return StaffGetModelToStaff(pageModel);
        }

        public async Task<CalabongaViewModel<Staff>> StaffGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/staff/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var pageModel = JsonSerializer.Deserialize<CalabongaViewModel<StaffGetModel>>(result, options);
            return StaffGetModelToStaff(pageModel);
        }
        
        public async Task<CalabongaGetPagedModel<Staff>> StaffGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/staff/get-paged");
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
            var pageModel = JsonSerializer.Deserialize<CalabongaGetPagedModel<StaffGetModel>>(result, options);
            return StaffGetPagedModelToStaff(pageModel);
        }
        
        private CalabongaViewModel<Staff> StaffGetModelToStaff(CalabongaViewModel<StaffGetModel> pageModel)
        {
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var returnModel = new CalabongaViewModel<Staff>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Staff()
                {
                    Id = new Guid(pageModel.Result.Id),
                    EmbedLink = pageModel.Result.EmbedLink,
                    Name = culturedName,
                    Role = (Models.StaffRoles) Enum.Parse(typeof(StaffRoles), pageModel.Result.Role),
                    Email = pageModel.Result.Email,
                    Text = culturedText
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Staff> StaffGetPagedModelToStaff(CalabongaGetPagedModel<StaffGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Staff>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Staff>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Staff>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var staff in pageModel.Result.Items)
                {
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(staff.Name);
                    var culturedText = JsonSerializer.Deserialize<CultureBase>(staff.Text);
                    returnModel.Result.Items.Add(new Staff()
                    {
                        Id = new Guid(staff.Id),
                        Name = culturedName,
                        EmbedLink = staff.EmbedLink,
                        Role = (Models.StaffRoles) Enum.Parse(typeof(StaffRoles), staff.Role),
                        Email = staff.Email,
                        Text = culturedText
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Staff>();
            }
        }
    }
}