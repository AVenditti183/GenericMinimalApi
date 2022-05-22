using IntroMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<FormFileOperationFilter>();
    options.OperationFilter<GetListOperationFilter>();
});
builder.Services.AddSingleton<DataStorage>();
builder.Services.AddScoped<IService<Blexiner>, BlexinerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/blexiners", (IService<Blexiner> service, GetListParameters parameters) => Results.Ok(service.GetList(parameters)))
.WithName("GetBlexiners")
.Produces(StatusCodes.Status200OK, typeof(IEnumerable<Blexiner>));

app.MapGet("/blexiners/{id}", (IService<Blexiner> service, Guid id) =>
{
    try
    {
        return Results.Ok(service.Get(id));
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("GetBlexiner")
.Produces(StatusCodes.Status200OK, typeof(Blexiner))
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/blexiners", (IService<Blexiner> service, Blexiner blexiner) =>
{
    var newBlexiner = service.Add(blexiner);
    return Results.CreatedAtRoute("GetBlexiner", new { newBlexiner.Id }, newBlexiner);
})
.WithName("PostBlexiner")
.Produces(StatusCodes.Status201Created, typeof(Blexiner));

app.MapPut("blexiners/{id:guid}", (IService<Blexiner> service, Guid id, Blexiner blexiner) =>
{
    try
    {
        service.Update(id, blexiner);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("PutBlexiner")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.MapDelete("blexiners/{id:guid}", (IService<Blexiner> service, Guid id) =>
{
    try
    {
        service.Delete(id);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("DeleteBlexiner")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.MapGet("blexiners/{id:guid}/photo", (IService<Blexiner> service, Guid id) =>
{
    try
    {
        return Results.Bytes(service.GetPhoto(id), "image/jpeg");
    }
    catch (FileNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("GetBlexinerPhoto")
.Produces(StatusCodes.Status200OK, contentType: "image/jpeg")
.Produces(StatusCodes.Status404NotFound);

app.MapPut("blexiners/{id:guid}/photo", (IService<Blexiner> service, Guid id, FormFileContent photo) =>
{
    try
    {
        service.AddPhoto(id, photo);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("PostBlexinerPhoto")
.Accepts<FormFileContent>("multipart/form-data")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.MapDelete("blexiners/{id:guid}/photo", (IService<Blexiner> service, Guid id) =>
{
    try
    {
        service.Delete(id);
        return Results.NoContent();
    }
    catch (FileNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("DeleteBlexinerPhoto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);

app.Run();