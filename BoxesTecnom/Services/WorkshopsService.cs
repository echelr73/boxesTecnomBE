using BoxesTecnom.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BoxesTecnom.Services
{
    public class WorkshopsService : IWorkshopsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "workshops_cache";
        private const string urlApi = "https://dev.tecnomcrm.com/api/v1/places/workshops";
        public WorkshopsService(HttpClient httpClient, IMemoryCache cache) 
        {
            _httpClient = httpClient;
            _cache = cache;

            var byteArray = Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<IEnumerable<Workshop>?> GetWorkshopsAsync()
        {

            if (_cache.TryGetValue(CACHE_KEY, out IEnumerable<Workshop> cachedData))
            {
                return cachedData!;
            }

            var response = await _httpClient.GetAsync(urlApi);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var workshops = JsonSerializer.Deserialize<IEnumerable<Workshop>>(json, options);

            // Save to cache
            _cache.Set(
                CACHE_KEY,
                workshops,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                }
            );

            return workshops.Where(w => w.IsActive);
        }
    }
}
