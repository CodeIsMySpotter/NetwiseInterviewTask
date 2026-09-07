using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Core.Services;
using WebApp.Core.Exceptions;

namespace WebApp.Core.Controllers;


[ApiController]
[Route("facts")]
public class FactsController(
    IExternalFetchService _fetchService,
    ILocalStorageService _storageService
) : ControllerBase
{

    [HttpPost("ask")]
    public async Task<IActionResult> AskFactEndpoint()
    {
        var response = await _fetchService.FetchResponseAsync();
        if (response?.Fact == null) throw new AskRequestErrorException("Failed to fetch data from the external API");
        await _storageService.SaveFactAsync(response.Fact);

        return Ok("Ok");
    }

    [HttpGet("file")]
    public async Task<IActionResult> GetTxtFile()
    {
        var content = await _storageService.GetFileAsync();
        return Ok(content);

    }
}




