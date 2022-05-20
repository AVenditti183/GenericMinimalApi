var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IEntityService<Blexiner>, BlexinerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/blexiners", (IEntityService<Blexiner> blexinerService, string searchText) => Results.Ok(blexinerService.GetList(searchText)))
.WithName("GetBlexiners")
.Produces(StatusCodes.Status200OK, typeof(IEnumerable<Blexiner>));

app.MapGet("/blexiners/{id}", (IEntityService<Blexiner> blexinerService, Guid id) =>
{
    try
    {
        return Results.Ok(blexinerService.Get(id));
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
.WithName("GetBlexiner")
.Produces(StatusCodes.Status200OK, typeof(Blexiner))
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/blexiners", (IEntityService<Blexiner> blexinerService, Blexiner blexiner) =>
{
    blexinerService.Add(blexiner);
    return Results.CreatedAtRoute("GetBlexiner", new { blexiner.Id }, blexiner.Id);
})
.WithName("PostBlexiner")
.Produces(StatusCodes.Status201Created, typeof(Guid));

app.MapPut("blexiners/{id:guid}", (IEntityService<Blexiner> blexinerService, Guid id, Blexiner blexiner) =>
{
    try
    {
        blexinerService.Update(id, blexiner);
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

app.MapDelete("", (IEntityService<Blexiner> blexinerService, Guid id) =>
{
    try
    {
        blexinerService.Delete(id);
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

app.Run();
