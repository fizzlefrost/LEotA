using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient
{
    public class EngineAuthenticationManager
    {
        private readonly HttpClient _httpClient;

        public EngineAuthenticationManager(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }


        public async Task<CalabongaViewModel<EngineRegisteredViewModel>> RegisterAsync(EngineRegisterModel registerViewModel, string password)
        {
            var request = new RegisterRequestTestModel()
            {
                RegisterModel = registerViewModel,
                Password = password
            };
            using var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                MediaTypeNames.Application.Json);
            using var response = await _httpClient.PostAsync($"/account/register", stringContent);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new InvalidOperationException($"Неожиданный ответ от EngineService {response.StatusCode}.{Environment.NewLine}{json}");
            var report = new CalabongaViewModel<EngineRegisteredViewModel>();
            try
            {
                report = JsonSerializer.Deserialize<CalabongaViewModel<EngineRegisteredViewModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception exception)
            {
                throw new JsonException($"Ошибка десериализации {Environment.NewLine}{json}", exception);
            }
            return report;
        }
    }

    public class RegisterRequestTestModel
    {
        public EngineRegisterModel RegisterModel { get; set; }
        public string Password { get; set; }
    }
}