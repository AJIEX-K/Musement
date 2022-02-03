using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Musement.Business;
using WeatherForecast;

namespace Musement
{
    class Program
    {
        private static ServiceProvider _serviceProvider { get; set; }
        public static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Type the command 'cities' please");
            var line = Console.ReadLine();
            var commandModule = Initialize();
            int res = await ExecuteCommand(commandModule, line);
            Console.Out.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
            return res;
        }

        private static async Task<int> ExecuteCommand(ICommandModule<string> commandModule, string line)
        {
            int res = 0;
            if (IsApplicationConfigurated(out var message))
            {
                res = await commandModule.ExecuteCommand(new string[] {line});
                Console.Out.WriteLine("Cities weather forecast has been loaded!");
            }
            else
                Console.Out.WriteLine(message);

            return res;
        }

        private static ICommandModule<string> Initialize()
        {
            var configuration = new Configuration.Configuration();
            _serviceProvider = configuration.ConfigurateServiceContainer();
            
            var commandModule = _serviceProvider.GetService<ICommandModule<string>>();
            var component = new CityWeatherComponentBuilder(_serviceProvider.GetService<ICityService>(),
                _serviceProvider.GetService<IWeatherForecastService>());
            commandModule.RegisterCommand("cities", () => component.Build().GetRequestResultAsync(),
                _serviceProvider.GetService<IPrintResult<string>>());
            return commandModule;
        }

        private static bool IsApplicationConfigurated(out string message)
        {
            message = string.Empty;
            if (_serviceProvider == null)
                return false;
            var config = _serviceProvider.GetService<IConfiguration>();
            var settings = config.GetSection("weatherForecast");
            if (string.IsNullOrEmpty(settings["key"]))
            {
                message = "WeatherForecast key is empty! Create your own account to get a key in weatherapi.com and update AppSetting";
                return false;
            }
            return true;

        }
    }
}
