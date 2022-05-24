using GenericMinimalApi.Data;
using GenericMinimalApi.Infrastructures;
using GenericMinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GenericMinimalApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CRUDBookController : CRUDController<Book, int, BookListDto, BookDto, BookDto, BookDto>
    {
        public CRUDBookController(ICRUDService<Book, int, BookListDto, BookDto, BookDto, BookDto> service) : base(service)
        {
        }

        protected override Func<string, Expression<Func<Book, bool>>>? TextFilterFunc 
            => (text) => book => book.Title.StartsWith(text) || book.Autor.StartsWith(text);
    }
}
