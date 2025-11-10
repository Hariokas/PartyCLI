using System.CommandLine;
using partycli.Commands.Interfaces;
using partycli.Services;

namespace partycli.Commands
{
    public class CliParser : ICliParser
    {
        private ServerListCommandHandler ServerListCommandHandler { get; }

        public CliParser(ServerListCommandHandler serverListCommandHandler)
        {
            ServerListCommandHandler = serverListCommandHandler;
        }

        public RootCommand BuildRootCommand()
        {
            var root = new RootCommand("PartyCLI")
            {
                CreateServerListCommand(),
                CreateConfigCommand()
            };

            return root;
        }

        private Command CreateServerListCommand()
        {
            var localOption = new Option<bool>("--local") { Description = "Display saved list of servers" };
            var franceOption = new Option<bool>("--france") { Description = "Get and save France servers" };
            var tcpOption = new Option<bool>("--TCP")
                { Description = "Get and save servers that support TCP protocol" };

            var command = new Command("server_list", "Fetch and save the list of VPN servers")
            {
                localOption,
                franceOption,
                tcpOption
            };

            command.SetAction(async parseResult =>
            {
                var local = parseResult.GetValue(localOption);
                var france = parseResult.GetValue(franceOption);
                var tcp = parseResult.GetValue(tcpOption);

                await ServerListCommandHandler.HandleAsync(local, france, tcp);
            });

            return command;
        }

        private Command CreateConfigCommand()
        {
            var nameArg = new Argument<string>("name") { Description = "Configuration name" };
            var valueArg = new Argument<string>("value") { Description = "Configuration value" };

            var command = new Command("config", "Set configuration values")
            {
                nameArg,
                valueArg
            };
            
            command.SetAction(parseResult =>
            {
                var name = parseResult.GetValue(nameArg);
                var value = parseResult.GetValue(valueArg);

                var processedName = name.Replace("-", string.Empty);
                StorageService.StoreValue(processedName, value);
                LogService.Log($"Changed {processedName} to {value}");
            });

            return command;
        }
    }
}