using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Musement.Business;
using Unity;
using WeatherForecast;

namespace Musement
{
    class Program
    {
        private static UnityContainer _container { get; set; }
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
            _container = configuration.Configurate();
            var commandModule = _container.Resolve<ICommandModule<string>>();
            var component = new CityWeatherComponentBuilder(_container.Resolve<ICityService>(),
                _container.Resolve<IWeatherForecastService>());
            commandModule.RegisterCommand("cities", () => component.Build().GetRequestResultAsync(),
                _container.Resolve<IPrintResult<string>>());
            return commandModule;
        }

        private static bool IsApplicationConfigurated(out string message)
        {
            message = string.Empty;
            if (_container == null)
                return false;
            var config = _container.Resolve<IConfiguration>();
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
