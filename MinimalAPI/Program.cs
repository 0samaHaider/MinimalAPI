var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


#region CRUD

app.MapGet("/api/greet", () => "Hello, Minimal API!");

app.MapPost("/api/post", (string data) => Results.Created("/api/post", data));

app.MapPut("/api/update", () => Results.Ok("Data updated successfully."));

app.MapDelete("/api/delete", () => Results.Ok("Data deleted successfully."));

#endregion

#region Dependency Injection

app.MapGet("/log", (ILogger<Program> logger) =>
{
    logger.LogInformation("Logging from Minimal API!");
    return Results.Ok("Check logs for information.");
});

#endregion


#region Async CRUD

app.MapGet("/api/Async/greet", async () => await Task.Run(() => "Hello, Minimal API!"));

app.MapPost("/api/Async/post", async (string data) =>
{
    // Simulating async operation
    await Task.Delay(1000);
    return Results.Created("/api/post", data);
});

app.MapPut("/api/Async/update", async () =>
{
    // Simulating async operation
    await Task.Delay(1000);
    return Results.Ok("Data updated successfully.");
});

app.MapDelete("/api/Async/delete", async () =>
{
    // Simulating async operation
    await Task.Delay(1000);
    return Results.Ok("Data deleted successfully.");
});

#endregion
app.Run();
