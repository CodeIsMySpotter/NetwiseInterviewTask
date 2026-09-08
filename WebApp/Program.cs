using WebApp.Services;
using WebApp.Infrastructure;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FactSettings>(builder.Configuration.GetSection("FactSettings"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<IExternalFetchService, ExternalFetchService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
