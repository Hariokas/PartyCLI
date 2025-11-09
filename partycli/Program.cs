using System.Threading.Tasks;
using partycli.Commands;

namespace partycli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var rootCommand = CliParser.BuildRootCommand();
            await rootCommand.Parse(args).InvokeAsync();
        }
    }
}