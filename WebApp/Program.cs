using WebApp.Services;
using WebApp.Infrastructure;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FactSettings>(builder.Configuration.GetSection("FactSettings"));

builder.Services.AddControllers();

builder.Services.AddHttpClient<IExternalFetchService, ExternalFetchService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
