using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApp.Controllers;


[ApiController]
[Route("[facts]")]
public class FactsController : ControllerBase
{

    [HttpGet(Name = "ask")]
    public async Task<IActionResult> AskFactEndpoint()
    {
        return Ok("Ok");
    }

    [HttpGet(Name = "file")]
    public string GetTxtFile()
    {
        return string.Empty;
    }
}




