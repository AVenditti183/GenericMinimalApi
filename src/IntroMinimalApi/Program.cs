using IntroMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DataStorage>();
builder.Services.AddScoped<IService<Blexiner>, BlexinerService>();
builder.Services.AddScoped<IValidator<Blexiner>, BlexinerValidator>();
//builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/blexiners", (IService<Blexiner> service, string searchText) => Results.Ok(service.GetList(searchText)))
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

app.MapPost("/blexiners", (IService<Blexiner> service, IValidator<Blexiner> validator, Blexiner blexiner) =>
{
    var validationResult = validator.Validate(blexiner);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());

        return Results.ValidationProblem(errors);
    }

    var newBlexiner = service.Add(blexiner);
    return Results.CreatedAtRoute("GetBlexiner", new { newBlexiner.Id }, newBlexiner);
})
.WithName("PostBlexiner")
.Produces(StatusCodes.Status201Created, typeof(Guid));

app.MapPut("blexiners/{id:guid}", (IService<Blexiner> service, IValidator<Blexiner> validator, Guid id, Blexiner blexiner) =>
{
    var validationResult = validator.Validate(blexiner);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());

        return Results.ValidationProblem(errors);
    }

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

app.Run();