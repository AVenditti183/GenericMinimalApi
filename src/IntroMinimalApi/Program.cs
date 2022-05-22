using IntroMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DataStorage>();
builder.Services.AddScoped<IService<Blexiner>, BlexinerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

MapEndpoints();

app.Run();

void MapEndpoints()
{
    app.MapGet("/blexiners", GetBlexiners)
    .WithName("GetBlexiners")
    .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Blexiner>));

    app.MapGet("/blexiners/{id}", GetBlexiner)
    .WithName("GetBlexiner")
    .Produces(StatusCodes.Status200OK, typeof(Blexiner))
    .Produces(StatusCodes.Status404NotFound);

    app.MapPost("/blexiners", PostBlexiner)
    .WithName("PostBlexiner")
    .Produces(StatusCodes.Status201Created, typeof(Blexiner));

    app.MapPut("blexiners/{id:guid}", PutBlexiner)
    .WithName("PutBlexiner")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

    app.MapDelete("blexiners/{id:guid}", DeleteBlexiner)
    .WithName("DeleteBlexiner")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);
}

IResult GetBlexiners(IService<Blexiner> service, string searchText)
{
    return Results.Ok(service.GetList(searchText));
}

IResult GetBlexiner(IService<Blexiner> service, Guid id)
{
    try
    {
        return Results.Ok(service.Get(id));
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
}

IResult PostBlexiner(IService<Blexiner> service, Blexiner blexiner)
{
    var newBlexiner = service.Add(blexiner);
    return Results.CreatedAtRoute("GetBlexiner", new { newBlexiner.Id }, newBlexiner);
}

IResult PutBlexiner(IService<Blexiner> service, Guid id, Blexiner blexiner)
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
}

IResult DeleteBlexiner(IService<Blexiner> service, Guid id)
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
}