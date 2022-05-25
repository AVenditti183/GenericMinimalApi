using GenericMinimalApi.Infrastructures;

namespace GenericMinimalApi.Data
{
    public class Book: IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Autor { get; set; }
    }
}
