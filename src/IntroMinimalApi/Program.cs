using IntroMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DataStorage>();
builder.Services.AddScoped<IService<Blexiner>, BlexinerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

new BlexinerHandler().MapEndpoints(app);

app.Run();