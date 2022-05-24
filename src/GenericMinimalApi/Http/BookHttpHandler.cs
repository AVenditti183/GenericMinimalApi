using GenericMinimalApi.Data;
using GenericMinimalApi.Infrastructures.Http;
using GenericMinimalApi.Models;

namespace GenericMinimalApi.Http
{
    public static class BookHttpHandler
    {
        public static void MapBook(this IEndpointRouteBuilder builder)
        {
            builder.CRUDHttpHandlerMap<Book,int, BookListDto, BookDto, BookDto, BookDto>( cfg =>
            {
                cfg.TextFilterFunc = (text) =>  book => book.Title.StartsWith(text) || book.Autor.StartsWith(text);
            });
        }
    }
}
