namespace GenericMinimalApi.Infrastructures
{
    public class Page<T>
    {
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public T[] Items { get; set; }
        public long ItemsCount { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirections OrderDirection { get; set; }
    }
}