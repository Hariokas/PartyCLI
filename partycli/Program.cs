using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using partycli.Commands.Interfaces;

namespace partycli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceProvider = ServiceConfiguration.ConfigureServices();

            try
            {
                var cliParser = serviceProvider.GetRequiredService<ICliParser>();
                var rootCommand = cliParser.BuildRootCommand();
                await rootCommand.Parse(args).InvokeAsync();
            }
            finally
            {
                if (serviceProvider is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

        }
    }
}