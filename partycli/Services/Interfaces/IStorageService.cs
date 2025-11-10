namespace partycli.Services.Interfaces
{
    public interface IStorageService
    {
        void StoreValue(string name, string value, bool writeToConsole = true);
        string GetValue(string name);
    }
}