using AIServices.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AIServices.Services
{
    public interface IAIService
    {
        Task<string> GetResponseAsync(string prompt);
    }



    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _settings;
        public AIService(HttpClient httpClient, IOptions<OpenAISettings> settings)//IOptions is a middleware that allows you to access configuration settings in a type-safe manner. It is commonly used in ASP.NET Core applications to retrieve configuration values from appsettings.json or other configuration sources.

        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<string> GetResponseAsync(string prompt)
        {
            var request = new RequestModel
            {
                model = _settings.Model,
                messages = new List<Message>
                {
            new Message {
               role = "user",
               content = prompt
            }

         }
            };
            string json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
            var response = await _httpClient.PostAsync(_settings.ApiUrl, content); // purpose of await is to asynchronously wait for the completion of the
          // PostAsync operation, which sends an HTTP POST request to the specified URL with the provided content. It allows the program to continue executing
          // other tasks while waiting for the response from the server,
           // //improving efficiency and responsiveness in applications that involve network communication.
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            var aiResponse = JsonSerializer.Deserialize<ResponseModel>(result);

            return aiResponse?.choices?.FirstOrDefault()?.message?.content ?? "No response received";
        }
    }
}
