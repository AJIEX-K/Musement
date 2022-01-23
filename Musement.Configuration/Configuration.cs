using System;
using Microsoft.Extensions.Configuration;
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
            RegisterType<IConfiguration>(container, () =>
            {
               return new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            });
            return container;
        }

        private void RegisterType<T2>(IUnityContainer unnityContainer, Func<T2> factory)
            where T2 : class
        {
            unnityContainer.RegisterType<T2, T2>(new InjectionFactory(((container, type, arg3) => factory())));
        }
    }
}
