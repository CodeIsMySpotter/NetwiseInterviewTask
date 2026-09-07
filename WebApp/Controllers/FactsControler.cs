using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApp.Services;

namespace WebApp.Controllers;


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
        if (response?.Fact == null) return BadRequest("Błąd pobierania danych z zewnętrznego API");
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




