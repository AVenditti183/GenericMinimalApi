using AutoMapper;
using GenericMinimalApi.Data;
using GenericMinimalApi.Models;

namespace GenericMinimalApi.Mapping
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookListDto>();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
