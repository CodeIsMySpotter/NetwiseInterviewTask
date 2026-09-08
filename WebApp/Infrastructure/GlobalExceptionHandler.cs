using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Exceptions;

namespace WebApp.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        logger.LogError(exception, "An unexpected error occurred: {Message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            AskRequestErrorException => (StatusCodes.Status404NotFound, "External API returned no facts"),
            HttpRequestException => (StatusCodes.Status502BadGateway, "Error communicating with the external API"),
            IOException => (StatusCodes.Status503ServiceUnavailable, "Problem accessing the file"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid data provided"),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected server error occurred")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
