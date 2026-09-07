using System.IO;
using System.Threading;

namespace WebApp.Core.Services;


public interface ILocalStorageService
{
    Task SaveFactAsync(string fact);
    Task<string> GetFileAsync();
}


public class LocalStorageService(IConfiguration config) : ILocalStorageService
{
    private readonly string _filePath = config["FactSettings:FilePath"] ?? "facts.txt";
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async Task SaveFactAsync(string fact)
    {
        await _semaphore.WaitAsync();
        try
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.AppendAllTextAsync(_filePath, fact + Environment.NewLine);
        }
        finally
        {
            _semaphore.Release();
        }
    }


    public async Task<string> GetFileAsync()
    {
        if (!File.Exists(_filePath))
        {
            return string.Empty;
        }

        await _semaphore.WaitAsync();
        try
        {
            return await File.ReadAllTextAsync(_filePath);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
