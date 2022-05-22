using IntroMinimalApi.Models;

namespace IntroMinimalApi.Handlers
{
    public class BlexinerHandler : IEndpointHandler
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/blexiners", GetList)
            .WithName("GetBlexiners")
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Blexiner>));

            app.MapGet("/blexiners/{id}", Get)
            .WithName("GetBlexiner")
            .Produces(StatusCodes.Status200OK, typeof(Blexiner))
            .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/blexiners", Post)
            .WithName("PostBlexiner")
            .Produces(StatusCodes.Status201Created, typeof(Blexiner));

            app.MapPut("blexiners/{id:guid}", Put)
            .WithName("PutBlexiner")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            app.MapDelete("blexiners/{id:guid}", Delete)
            .WithName("DeleteBlexiner")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }

        private IResult GetList(IService<Blexiner> service, string searchText)
        {
            return Results.Ok(service.GetList(searchText));
        }

        private IResult Get(IService<Blexiner> service, Guid id)
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

        private IResult Post(IService<Blexiner> service, Blexiner blexiner)
        {
            var newBlexiner = service.Add(blexiner);
            return Results.CreatedAtRoute("GetBlexiner", new { newBlexiner.Id }, newBlexiner);
        }

        private IResult Put(IService<Blexiner> service, Guid id, Blexiner blexiner)
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

        private IResult Delete(IService<Blexiner> service, Guid id)
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
    }
}
