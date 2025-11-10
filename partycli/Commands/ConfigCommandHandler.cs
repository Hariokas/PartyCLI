using partycli.Services.Interfaces;

namespace partycli.Commands
{
    public class ConfigCommandHandler
    {
        private IStorageService StorageService { get; }
        private ILogService LogService { get; }

        public ConfigCommandHandler(IStorageService storageService, ILogService logService)
        {
            StorageService = storageService;
            LogService = logService;
        }

        public void Handle(string name, string value)
        {
            var processedName = name.Replace("-", string.Empty);
            StorageService.StoreValue(processedName, value);
            LogService.Log($"Changed {processedName} to {value}");
        }
    }
}
