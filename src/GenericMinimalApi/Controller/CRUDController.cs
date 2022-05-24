using GenericMinimalApi.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GenericMinimalApi.Controller
{
    [ApiController]
    public abstract class CRUDController<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> : ControllerBase
    where TEntity : IEntity<TKey>
    where TListItem : IEntityDto<TKey>
    where TGetItem : IEntityDto<TKey>
    where TPostITem : IEntityDto<TKey>
    where TPutItem : IEntityDto<TKey> 
    {
        private readonly ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service;

        public CRUDController(ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service)
        {
            this.service=service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Search([FromQuery] SearchParameters parameters)
        {
            return Ok( await service.Search(parameters, TextFilterFunc, User));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey id)
        {
            var entity = await service.Get(id,User);
            return entity is null ? Ok(entity): NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TPostITem item)
        {
            await service.Create(item, User);
            return NoContent();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TKey id, [FromBody] TPutItem item)
        {

            if( (await service.Get(id,User)) is null)
                return NotFound();

            await service.Update(item, id, User);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {

            if ((await service.Get(id, User)) is null)
                return NotFound();

            await service.Delete(id, User);
            return NoContent();
        }

        protected virtual Func<string, Expression<Func<TEntity, bool>>>? TextFilterFunc
            => null;
    }
}
