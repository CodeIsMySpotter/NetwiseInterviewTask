using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Exceptions;

namespace WebApp.Controllers;

[ApiController]
[Route("facts")]
public class FactsController(
    IExternalFetchService fetchService,
    ILocalStorageService storageService
) : ControllerBase
{
    [HttpPost("ask")]
    public async Task<IActionResult> AskFactEndpoint()
    {
        var response = await fetchService.FetchResponseAsync();
        if (response?.Fact == null) throw new AskRequestErrorException("Failed to fetch data from the external API");
        await storageService.SaveFactAsync(response.Fact);

        return Ok("Ok");
    }

    [HttpGet("file")]
    public IActionResult GetTxtFile()
    {
        var stream = storageService.GetFileStream();
        if (stream == null) return Ok(string.Empty);

        return File(stream, "text/plain", "facts.txt");
    }
}
