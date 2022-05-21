using GenericMinimalApi.Data;
using GenericMinimalApi.Infrastructures;

namespace GenericMinimalApi.Repository
{
    public class BookRepository : IRepository<Book, int>
    {
        private static List<Book> books;
        public BookRepository()
        {
            if(books is null)
                books= new List<Book>()
                {
                    new Book
                    {
                        Id =1,
                        Title="Harry Potter",
                        Autor="J.K. Rowling"
                    }
                };

        }
        public Task Create(Book item)
        {
            var lastId = books.Max( s => s.Id);
            item.Id = lastId + 1;
            books.Add(item);
            return Task.CompletedTask;
        }

        public Task Delete(int key)
        {
            var book = books.FirstOrDefault(x => x.Id == key);
            if(book is not null)
                books.Remove( book);

            return Task.CompletedTask;
        }

        public Task<Book> Get(int key)
        {
            return Task.FromResult( books.FirstOrDefault(x => x.Id == key));
        }

        public Task<IQueryable<Book>> GetAll()
        {
            return Task.FromResult(books.AsQueryable());
        }

        public Task Update(Book item)
        {
            var book = books.FirstOrDefault( x=> x.Id == item.Id);

            if(book is not null)
            {
                book.Autor = item.Autor;
                book.Title = item.Title;
            }

            return Task.CompletedTask;
        }
    }
}
