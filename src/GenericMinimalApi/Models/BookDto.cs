using GenericMinimalApi.Infrastructures;

namespace GenericMinimalApi.Models
{

    public class BookDto:IEntityDto<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Autor { get; set; }
    }
}
