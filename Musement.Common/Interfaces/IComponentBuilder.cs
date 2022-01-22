namespace WeatherForecast
{
    public interface IComponentBuilder<T>
    {
        /// <summary>
        /// Build component result
        /// </summary>
        /// <returns></returns>
        IComponentResultBuilder<T> Build();
    }
}