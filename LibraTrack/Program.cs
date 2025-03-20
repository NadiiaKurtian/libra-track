using LibraTrack.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 

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
