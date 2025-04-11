namespace Ecommerce.Application.Common.Models
{
    public class PaginatedResult<T> where T : class
    {
        public PaginatedResult(IList<T> data, int currentPage, int pageSize, int count)
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public IList<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
