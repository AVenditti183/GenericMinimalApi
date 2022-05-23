using GenericMinimalApi.Infrastructures;
using System.ComponentModel.DataAnnotations;

namespace GenericMinimalApi.Models
{

    public class BookDto:IEntityDto<int>
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Autor { get; set; }
    }
}
