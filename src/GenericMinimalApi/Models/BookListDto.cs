using GenericMinimalApi.Infrastructures;

namespace GenericMinimalApi.Models
{
    public class BookListDto:IEntityDto<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
