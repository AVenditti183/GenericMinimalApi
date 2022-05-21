namespace IntroMinimalApi.Binding
{
    public class GetListParameters
    {
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }

        public static ValueTask<GetListParameters> BindAsync(HttpContext httpContext, ParameterInfo parameter)
        {
            string searchName = httpContext.Request.Query["searchtext"];
            int.TryParse(httpContext.Request.Query["pagenumber"], out var pageNumber);
            int.TryParse(httpContext.Request.Query["pagesize"], out var pageSize);
            string orderBy = httpContext.Request.Query["orderby"];

            return ValueTask.FromResult<GetListParameters>(
                new GetListParameters
                {
                    SearchText = searchName,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    OrderBy = orderBy
                }
            );
        }
    }
}
