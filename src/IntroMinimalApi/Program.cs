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

app.MapGet("/hellowo", () => Results.Ok(new { traceLevel = 0, errorDesc = "", command =  });
app.MapPost("/h")
app.Run();
