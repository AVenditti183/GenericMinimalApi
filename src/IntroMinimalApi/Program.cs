// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

var builder = WebApplication.CreateBuilder(args);

// 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/helloworld", () => "Hello world!");
app.Run();
