using BoxesTecnom.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BoxesTecnom.Services
{
    public class WorkshopsService : IWorkshopsService
    {
        private readonly HttpClient _httpClient;
        private const string urlApi = "https://dev.tecnomcrm.com/api/v1/places/workshops";
        public WorkshopsService(HttpClient httpClient) 
        {
            _httpClient = httpClient;

            var byteArray = Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<IEnumerable<Workshop>?> GetWorkshopsAsync()
        {
            var response = await _httpClient.GetAsync(urlApi);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var workshops = JsonSerializer.Deserialize<IEnumerable<Workshop>>(json, options);

            return workshops.Where(w => w.IsActive);
        }
    }
}
