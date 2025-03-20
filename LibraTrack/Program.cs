using LibraTrack.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<AuthorService>();
builder.Services.AddSingleton<BookService>();

var app = builder.Build();

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

app.MapGet("/welcome/{name?}", async context =>
{
    var name = context.Request.RouteValues["name"]?.ToString() ?? "Guest";
    await context.Response.WriteAsync($"Welcome, {name}!");
});

app.MapGet("/api/book/{author}/{year:int}", async context =>
{
    var author = context.Request.RouteValues["author"]?.ToString();
    var year = context.Request.RouteValues["year"]?.ToString();
    await context.Response.WriteAsync($"Books by {author} published in {year}");
});

app.MapGet("/books/{title:minlength(3)}", async context =>
{
    var title = context.Request.RouteValues["title"]?.ToString();
    await context.Response.WriteAsync($"Book: {title}");
});

app.Run();
