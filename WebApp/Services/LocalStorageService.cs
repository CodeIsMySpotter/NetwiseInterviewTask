using System.IO;
using System.Threading;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using WebApp.Models;

namespace WebApp.Services;

public interface ILocalStorageService
{
    Task SaveFactAsync(string fact);
    Stream? GetFileStream();
}

public class LocalStorageService(IOptions<FactSettings> options, IWebHostEnvironment env) : ILocalStorageService
{
    private readonly string _filePath = Path.Combine(env.ContentRootPath, options.Value.FilePath);
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

    public Stream? GetFileStream()
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        return new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    }
}
