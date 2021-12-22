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
    public class ProjectPatron : IProjectPatron
    {
        private HttpClient _httpClient;

        public ProjectPatron(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Engine");
        }

        public async Task<CalabongaViewModel<Project>> ProjectPostAsync(ProjectCreateModel ProjectCreateModel)
        {
            var request = new ProjectCreateModel()
            {
                Name = ProjectCreateModel.Name,
                EmbedLink = ProjectCreateModel.EmbedLink,
                Text = ProjectCreateModel.Text
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/project/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ProjectGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return ProjectGetModelToProject(report);
        }

        public async Task<CalabongaViewModel<Project>> ProjectPutAsync(ProjectUpdateModel ProjectUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/project/post-item", ProjectUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ProjectGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return ProjectGetModelToProject(report);
        }
        
        public async Task<CalabongaViewModel<Project>> ProjectDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/project/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ProjectGetModel>>(result, options);
            return ProjectGetModelToProject(report);
        }

        public async Task<CalabongaViewModel<Project>> ProjectGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/project/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<ProjectGetModel>>(result, options);
            return ProjectGetModelToProject(report);
        }
        
        public async Task<CalabongaGetPagedModel<Project>> ProjectGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/project/get-paged");
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<ProjectGetModel>>(result, options);
            return ProjectGetPagedModelToProject(report);
        }
        
        private CalabongaViewModel<Project> ProjectGetModelToProject(CalabongaViewModel<ProjectGetModel> pageModel)
        {
            var culturedName = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Name);
            var culturedText = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Text);
            var returnModel = new CalabongaViewModel<Project>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new Project()
                {
                    
                    Id = new Guid(pageModel.Result.Id),
                    Name = culturedName,
                    Text = culturedText,
                    EmbedLink = pageModel.Result.EmbedLink
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<Project> ProjectGetPagedModelToProject(CalabongaGetPagedModel<ProjectGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<Project>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<Project>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<Project>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var Project in pageModel.Result.Items)
                {
                    var culturedName = JsonSerializer.Deserialize<CultureBase>(Project.Name);
                    var culturedText = JsonSerializer.Deserialize<CultureBase>(Project.Text);
                    returnModel.Result.Items.Add(new Project()
                    {
                        Id = new Guid(Project.Id),
                        Name = culturedName,
                        Text = culturedText,
                        EmbedLink = Project.EmbedLink
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<Project>();
            }
        }
    }
}