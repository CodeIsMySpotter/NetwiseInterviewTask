using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using WebApp.Models;
using WebApp.Exceptions;

namespace WebApp.Services;

public interface IExternalFetchService
{
    Task<ExternalResponse> FetchResponseAsync();
}

public class ExternalFetchService(HttpClient httpClient, IOptions<FactSettings> options) : IExternalFetchService
{
    public async Task<ExternalResponse> FetchResponseAsync()
    {
        var url = options.Value.ApiUrl;
        var response = await httpClient.GetFromJsonAsync<ExternalResponse>(url);
        if (response == null) throw new AskRequestErrorException("External API fetch failed");
        return response;
    }
}
