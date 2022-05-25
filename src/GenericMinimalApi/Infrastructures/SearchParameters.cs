using System.Text.Json.Serialization;

namespace GenericMinimalApi.Infrastructures
{
    public class SearchParameters
    {
        public SearchParameters()
        {

        }

        public SearchParameters(string textFilter, string orderBy, OrderDirections orderDirection, int page, int pageSize)
        {
            TextFilter = textFilter;
            OrderBy = orderBy;
            OrderDirection = orderDirection;
            Page = page;
            PageSize = pageSize;
        }

        public string? TextFilter { get;set;}
        public string? OrderBy { get; set; }
        public OrderDirections OrderDirection{ get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }



    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderDirections
    {
        Ascendent,
        Descendent
    }
}