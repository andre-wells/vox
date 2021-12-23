using Microsoft.Extensions.Options;
using Question4.Configuration;
using Question4.Data;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Question4.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<NewsServiceOptions> _options;

        public NewsService(
            HttpClient httpClient,
            IOptions<NewsServiceOptions> options
            )
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<IEnumerable<NewsItem>> GetNewsEventsAsync()
        {
            var response = await _httpClient.GetAsync("https://pastebin.com/raw/7RJzFSqW");
            response.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<IEnumerable<NewsItem>>(
                await response.Content.ReadAsStreamAsync());
        }
    }
}
