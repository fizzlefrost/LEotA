using System;
using System.Collections.Generic;
using System.IO;
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
    public class FileContentPatron : IFileContentPatron
    {
        private HttpClient _httpClient;

        public FileContentPatron(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Engine");
        }
        
        public async Task<CalabongaViewModel<FileContent>> FileContentPostAsync(FileContentCreateModel FileContentCreateModel)
        {
            var request = new FileContentCreateModel()
            {
                Content = FileContentCreateModel.Content,
                MasterId = FileContentCreateModel.MasterId,
                MimeType = FileContentCreateModel.MimeType,
                Author = FileContentCreateModel.Author,
                FileType = FileContentCreateModel.FileType
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/api/FileContent/post-item", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<FileContentGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return FileContentGetModelToFileContent(report);
        }

        public async Task<CalabongaViewModel<FileContent>> FileContentPutAsync(FileContentUpdateModel FileContentUpdateModel)
        {
            using var response = await _httpClient.PutAsJsonAsync($"/api/FileContent/post-item", FileContentUpdateModel);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = JsonSerializer.Deserialize<CalabongaViewModel<FileContentGetModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return FileContentGetModelToFileContent(report);
        }
        
        public async Task<CalabongaViewModel<FileContent>> FileContentDeleteAsync(string id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/FileContent/delete-item/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<FileContentGetModel>>(result, options);
            return FileContentGetModelToFileContent(report);
        }

        public async Task<CalabongaViewModel<FileContent>> FileContentGetByIdAsync(string id)
        {
            var httpResponse = await _httpClient.GetAsync($"/api/FileContent/get-by-id/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var report = JsonSerializer.Deserialize<CalabongaViewModel<FileContentGetModel>>(result, options);
            return FileContentGetModelToFileContent(report);
        }
        
        public async Task<CalabongaGetPagedModel<FileContent>> FileContentGetPagedAsync(CalabongaGetPagedRequestModel parameters)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}api/FileContent/get-paged");
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
            var report = JsonSerializer.Deserialize<CalabongaGetPagedModel<FileContentGetModel>>(result, options);
            return FileContentGetPagedModelToFileContent(report);
        }

        public async Task<List<FileContent>> FileContentGetByMasterIdAsync(string id)
        {
            var builder = new UriBuilder($"{_httpClient.BaseAddress}file-by-master-id");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = id;
            builder.Query = query.ToString() ?? string.Empty;
            string url = builder.ToString();
            var httpResponse = await _httpClient.GetAsync(url);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            // var report = JsonSerializer.Deserialize<CalabongaViewModel<List<FileContentGetModel>>>(result, options);
            var report = JsonSerializer.Deserialize<List<FileContentGetModel>>(result, options);
            // var ret = new CalabongaViewModel<List<FileContent>>()
            // {
            //     ActivityId = report.ActivityId,
            //     Exception = report.Exception,
            //     Logs = report.Logs,
            //     Metadata = report.Metadata,
            //     Ok = report.Ok,
            //     Result = new List<FileContent>()
            // };
            var ret = new List<FileContent>();
            foreach (var fileContentGetModel in report)
            {
                try
                {
                    var culturedAuthor = new CultureBase()
                    {
                        English = "System",
                        Russian = "Система"
                    };
                    if (fileContentGetModel.Author != null)
                    {
                        try
                        {
                            culturedAuthor = JsonSerializer.Deserialize<CultureBase>(fileContentGetModel.Author);
                        }
                        catch (Exception e)
                        {
                            culturedAuthor = new CultureBase()
                            {
                                English = "Invalid author",
                                Russian = "Некорректно заполненное поле 'Автор'"
                            };
                        }
                    }
                    ret.Add(new FileContent()
                    {
                        Id = new Guid(fileContentGetModel.Id),
                        MimeType = fileContentGetModel.MimeType,
                        Content = Convert.FromBase64String(fileContentGetModel.Content),
                        MasterId = new Guid(fileContentGetModel.MasterId),
                        Author = culturedAuthor,
                        FileType = (FileType) fileContentGetModel.FileType
                    });
                }
                catch (Exception e)
                {
                    
                }
            }
            return ret;
        }
        
        private CalabongaViewModel<FileContent> FileContentGetModelToFileContent(CalabongaViewModel<FileContentGetModel> pageModel)
        {
            var culturedAuthor = JsonSerializer.Deserialize<CultureBase>(pageModel.Result.Author!);
            var returnModel = new CalabongaViewModel<FileContent>()
            {
                ActivityId = pageModel.ActivityId,
                Exception = pageModel.Exception,
                Logs = pageModel.Logs,
                Metadata = pageModel.Metadata,
                Ok = pageModel.Ok,
                Result = new FileContent()
                {
                    Id = new Guid(pageModel.Result.Id),
                    MimeType = pageModel.Result.MimeType,
                    Content = Convert.FromBase64String(pageModel.Result.Content),
                    MasterId = new Guid(pageModel.Result.MasterId),
                    Author = culturedAuthor,
                    FileType = (FileType) pageModel.Result.FileType
                }
            };
            return returnModel;
        }

        private CalabongaGetPagedModel<FileContent> FileContentGetPagedModelToFileContent(CalabongaGetPagedModel<FileContentGetModel> pageModel)
        {
            try
            {
                var returnModel = new CalabongaGetPagedModel<FileContent>()
                {
                    ActivityId = pageModel.ActivityId,
                    Exception = pageModel.Exception,
                    Logs = pageModel.Logs,
                    Metadata = pageModel.Metadata,
                    Ok = pageModel.Ok,
                    Result = new Page<FileContent>()
                    {
                        HasNextPage = pageModel.Result.HasNextPage,
                        HasPreviousPage = pageModel.Result.HasPreviousPage,
                        IndexFrom = pageModel.Result.IndexFrom,
                        Items = new List<FileContent>(),
                        PageIndex = pageModel.Result.PageIndex,
                        PageSize = pageModel.Result.PageSize,
                        TotalCount = pageModel.Result.TotalCount,
                        TotalPages = pageModel.Result.TotalPages
                    }
                };
                foreach (var fileContent in pageModel.Result.Items)
                {
                    var culturedAuthor = JsonSerializer.Deserialize<CultureBase>(fileContent.Author!);
                    returnModel.Result.Items.Add(new FileContent()
                    {
                        Id = new Guid(fileContent.Id),
                        MimeType = fileContent.MimeType,
                        Content = Convert.FromBase64String(fileContent.Content),
                        MasterId = new Guid(fileContent.MasterId),
                        Author = culturedAuthor,
                        FileType = (FileType) fileContent.FileType
                    });
                }

                return returnModel;
            }
            catch (Exception e)
            {
                return new CalabongaGetPagedModel<FileContent>();
            }
        }
    }
}