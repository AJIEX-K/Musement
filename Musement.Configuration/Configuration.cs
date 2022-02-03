using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Musement.Business;
using Musement.Command;
using Unity;
using Unity.Injection;
using WeatherForecast;

namespace Musement.Configuration
{
    public class Configuration
    {
        public UnityContainer Configurate()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<ICityService, CityService>();
            container.RegisterType<IWeatherForecastService, WeatherForecastService>();
            container.RegisterType(typeof(IPrintResult<>), typeof(CityWeatherForecastPrintResult), new InjectionConstructor());
            container.RegisterType(typeof(ICommandModule<>), typeof(CommandModule), new InjectionConstructor());
            container.RegisterFactory<IConfiguration>(x => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            return container;
        }

        public ServiceProvider ConfigurateServiceContainer()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(x => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            serviceCollection.AddTransient<ICityService, CityService>();
            serviceCollection.AddTransient<IWeatherForecastService, WeatherForecastService>();
            serviceCollection.AddTransient(typeof(IPrintResult<string>), typeof(CityWeatherForecastPrintResult));
            serviceCollection.AddTransient(typeof(ICommandModule<string>), typeof(CommandModule));
            return serviceCollection.BuildServiceProvider();
        }
    }
}
