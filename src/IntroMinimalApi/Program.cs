// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world!");

app.Run();
