// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

var app = WebApplication.Create(args);

app.MapGet("/", () => "Hello world!");

app.Run();
