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
        // Note: In a production system, this ApiKey should be loaded from an appsettings.json file.
        // This particular key will eventually become inactive.
        const string ApiKey = "AIzaSyAOT2CYIlZW0nifJQpZiJ-rIRNGX1YnhPw";
        private readonly HttpClient _httpClient;

        public GetCountryHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCountryAsync(string latitude, string longitude)
        {
            var response = await _httpClient.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?key={ApiKey}&latlng={latitude},{longitude}&sensor=false");
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
