using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Threading.Tasks;
using WeatherForecast;

namespace Musement.Command
{
    public class CommandModule : ICommandModule<string>
    {
        private RootCommand _root;
        public CommandModule()
        {
            _root = new RootCommand();
            _root.Name = "TUI_Musement";
            _root.Description = "Weather forecast Tool";
        }

        public async Task<int> ExecuteCommand(string[] args)
        {
            return await _root.InvokeAsync(args);
        }

        public void RegisterCommand(string commandName, Func<Task<IEnumerable<string>>> action, IPrintResult<string> printResult)
        {
            var citiesCommand = new System.CommandLine.Command(commandName);
            citiesCommand.Handler = CommandHandler.Create<string>(async x =>
            {
                if (action == null)
                    return;
                Console.WriteLine("Wait please...");
                printResult.PrintResult(await action.Invoke());
            });
            _root.AddCommand(citiesCommand);
        }
    }
}
