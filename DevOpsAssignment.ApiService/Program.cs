var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddNpgsqlDataSource(connectionName: "appdb");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<DevOpsAssignment.ApiService.DBService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries =
    ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapGet("/greetings", async (DevOpsAssignment.ApiService.DBService dbService) =>
    {
        var greetings = await dbService.GetAllGreetingsAsync();
        return greetings.Select(g => new { id = g.Id, message = g.Message });
    })
    .WithName("GetAllGreetings");

app.MapGet("/greeting", async (DevOpsAssignment.ApiService.DBService dbService) =>
    {
        var message = await dbService.GetGreetingMessageAsync();
        return message ?? "No greeting found";
    })
    .WithName("GetGreeting")
    .Produces<string>();

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}