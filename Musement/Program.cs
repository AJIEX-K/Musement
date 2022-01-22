using System;
using System.Threading.Tasks;
using Musement.Business;
using Unity;
using WeatherForecast;

namespace Musement
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var configuration = new Configuration.Configuration();
            var container = configuration.Configurate();
            var commandModule = container.Resolve<ICommandModule<string>>();
            var component = new CityWeatherComponentBuilder(container.Resolve<ICityService>(), container.Resolve<IWeatherForecastService>());
            commandModule.RegisterCommand("cities", () => component.Build().GetRequestResultAsync(), container.Resolve<IPrintResult<string>>());
            var res = await commandModule.ExecuteCommand(args);

            Console.ReadKey(true);
            return res;
        }


    }
}
