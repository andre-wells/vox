using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Question2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args != null 
                && args.Length == 2 
                && double.TryParse(args[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat) 
                && double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon))
            {
                var sp = new ServiceCollection()
                    .AddHttpClient()
                    .AddTransient<GetCountryHandler>()
                    .BuildServiceProvider();

                await sp.GetRequiredService<GetCountryHandler>().GetCountryAsync(1, 2);
            }
            else
                Console.WriteLine("Invalid arguments.");
        }

    }
}
