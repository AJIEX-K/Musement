using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public abstract class BaseComponentResultBuilder<T, TRes, TP> : IComponentResultBuilder<T>
    {
        protected IComponentResultBuilder<T> Component;

        protected BaseComponentResultBuilder()
        {
            
        }

        public BaseComponentResultBuilder(IComponentResultBuilder<T> component)
        {
            Component = component;
        }

        public abstract Task<IEnumerable<string>> GetRequestResultAsync();

        public abstract Task<IRequestResult<TRes>> GetDataAsync(params TP[] parameters);

        protected abstract Task<string> GetRequestResultAsync(T res);

        public async Task<string> GetResultAsync(T res)
        {
            return await GetRequestResultAsync(res);
        }
    }

    
}