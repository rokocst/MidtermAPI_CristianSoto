using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Part D: Stateful Request Counter
var requestCounts = new ConcurrentDictionary<string, int>();

// Part A: In-memory Data Store
var items = new List<Item>
{
    new Item { Id = 1, Name = "Sample Item", Quantity = 10 }
};

var app = builder.Build();

// Enable Swagger for Part E testing
app.UseSwagger();
app.UseSwaggerUI();

// --- PART B: Exception Handling Middleware ---
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Error]: {ex.Message}");
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { 
            error = "ServerError", 
            message = "An unexpected error occurred." 
        });
    }
});

// --- PART C: API Key Security Middleware ---
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? "";
    // Protect /items and /usage, but allow Swagger so you can see the UI
    if (path.Contains("/items") || path.Contains("/usage"))
    {
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) || 
            extractedApiKey != "MIDTERM_KEY_123")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { 
                error = "Unauthorized", 
                message = "Invalid or missing API key." 
            });
            return;
        }
    }
    await next();
});

// --- ENDPOINTS ---

app.MapGet("/items", (bool? fail) =>
{
    if (fail == true) throw new Exception("Midterm manual fail triggered.");
    return Results.Ok(items);
});

app.MapPost("/items", (Item newItem) =>
{
    if (string.IsNullOrWhiteSpace(newItem.Name))
        return Results.BadRequest(new { error = "InvalidItem", message = "Name cannot be empty" });
    
    if (newItem.Quantity < 0)
        return Results.BadRequest(new { error = "InvalidItem", message = "Quantity must be zero or greater" });

    newItem.Id = items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
    items.Add(newItem);
    return Results.Created($"/items/{newItem.Id}", new { message = "Item created", item = newItem });
});

app.MapGet("/usage", (HttpContext context) =>
{
    var apiKey = context.Request.Headers["X-Api-Key"].ToString();
    var count = requestCounts.AddOrUpdate(apiKey, 1, (key, oldValue) => oldValue + 1);
    return Results.Ok(new { apiKey = apiKey, count = count });
});

app.Run();

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}