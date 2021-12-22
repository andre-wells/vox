using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Question2
{
    internal class GetCountryHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<GoogleOptions> _options;

        public GetCountryHandler(
            HttpClient httpClient,
            IOptions<GoogleOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<string> GetCountryAsync(string latitude, string longitude)
        {
            var apikey = _options.Value.GeocodingApiKey;

            var response = await _httpClient.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?key={apikey}&latlng={latitude},{longitude}&sensor=false");
            response.EnsureSuccessStatusCode();

            var rootObj = JsonSerializer.Deserialize<Rootobject>(await response.Content.ReadAsStringAsync());

            if (rootObj.status.ToUpper() == "OK")
            {
                var streetAddressResult = rootObj.results.FirstOrDefault(x => x.types.Any(y => y == "street_address"));
                if (streetAddressResult != null)
                {
                    var countryAddressComponent = streetAddressResult.address_components.FirstOrDefault(x => x.types.Any(y => y == "country"));
                    if (countryAddressComponent != null)
                        return countryAddressComponent.long_name;
                }
                
                return "Could not identify Country from arguments.";
            }
            
            return "No Results";
        }
    }
}
