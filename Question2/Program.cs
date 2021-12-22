using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Question2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args != null 
                && args.Length == 2 
                && double.TryParse(args[0], NumberStyles.Any, CultureInfo.InvariantCulture, out _) 
                && double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                var sp = new ServiceCollection()
                    .AddHttpClient()                    
                    .AddTransient<GetCountryHandler>()
                    .AddOptions()
                    .Configure<GoogleOptions>(ops => config.GetSection("Google").Bind(ops))
                    .BuildServiceProvider();

                Console.WriteLine(await sp.GetRequiredService<GetCountryHandler>().GetCountryAsync(args[0], args[1]));
            }
            else
                Console.WriteLine("Invalid arguments.");
        }

    }
}
