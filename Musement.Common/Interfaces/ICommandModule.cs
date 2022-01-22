using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast
{
    public interface ICommandModule<T>
    {
        Task<int> ExecuteCommand(string[] args);

        /// <summary>
        /// Registration of command with action
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="action"></param>
        /// <param name="printResult"></param>
        void RegisterCommand(string commandName, Func<Task<IEnumerable<T>>> action, IPrintResult<T> printResult);
    }
}