using WebApp.Core.DataTransferObject;
using System.Net.Http.Json;
using WebApp.Core.Exceptions;
namespace WebApp.Core.Services;


public interface IExternalFetchService
{
    Task<ExternalResponse> FetchResponseAsync();

}


public class ExternalFetchService(HttpClient httpClient, IConfiguration config) : IExternalFetchService
{
    public async Task<ExternalResponse> FetchResponseAsync()
    {
        var url = config["FactSettings:ApiUrl"] ?? "https://catfact.ninja/fact";
        var response = await httpClient.GetFromJsonAsync<ExternalResponse>(url);
        if (response == null) throw new AskRequestErrorException("External API fetch failed");
        return response;
    }
}
