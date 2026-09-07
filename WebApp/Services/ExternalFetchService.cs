using WebApp.DataTransferObject;
using System.Net.Http.Json;
namespace WebApp.Services;


public interface IExternalFetchService
{
    Task<ExternalResponse> FetchResponseAsync();

}


public class ExternalFetchService(HttpClient httpClient) : IExternalFetchService
{
    public async Task<ExternalResponse> FetchResponseAsync()
    {
        var url = "https://catfact.ninja/fact";
        var response = await httpClient.GetFromJsonAsync<ExternalResponse>(url);
        return response;
    }
}