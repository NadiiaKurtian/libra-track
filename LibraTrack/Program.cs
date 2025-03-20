using LibraTrack.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<BookService>();

// ✅ Додаємо Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Включаємо Swagger тільки в режимі розробки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); // Використовує атрибутну маршрутизацію

// ✅ Додаємо конвенційні маршрути
app.MapGet("/greet", async context =>
{
    await context.Response.WriteAsync("Welcome to LibraTrack API!");
});

app.MapGet("/author/{name}", async context =>
{
    var name = context.Request.RouteValues["name"]?.ToString();
    await context.Response.WriteAsync($"Author: {name}");
});

app.Run();
