using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Question2
{
    internal class GetCountryHandler
    {
        const string Key = "AIzaSyAOT2CYIlZW0nifJQpZiJ-rIRNGX1YnhPw";

        public GetCountryHandler(HttpClient httpClient)
        {

        }

        // 
        public async Task<string> GetCountryAsync(double latitude, double longitude)
        {
            Console.WriteLine("hi");

            return null;
        }
    }
}
