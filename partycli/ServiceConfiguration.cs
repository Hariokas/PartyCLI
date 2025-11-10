using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using partycli.Commands;
using partycli.Commands.Interfaces;
using partycli.Services;
using partycli.Services.Interfaces;

namespace partycli
{
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(provider =>
            {
                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                httpClient.BaseAddress = new Uri("https://api.nordvpn.com/");
                return httpClient;
            });

            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<IConsoleDisplay, ConsoleDisplay>();
            services.AddSingleton<ILogService, LogService>();

            services.AddTransient<ServerListCommandHandler>();
            services.AddTransient<ConfigCommandHandler>();
            services.AddTransient<ICliParser, CliParser>();

            return services.BuildServiceProvider();
        }
    }
}