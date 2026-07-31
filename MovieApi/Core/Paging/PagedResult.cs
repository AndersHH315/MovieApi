namespace MovieApi.Core.Paging
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = [];

        public PagingMeta Meta { get; set; } = new();

    }

    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResult<T>(this IQueryable<T> query, int currentPage, int pageSize)
        {
             var totalItems = query.Count();
             var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
             var data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
             return new PagedResult<T>
               {
                  Data = data,
                  Meta = new PagingMeta
                  {
                      TotalItems = totalItems,
                      CurrentPage = currentPage,
                      TotalPages = totalPages,
                      PageSize = pageSize,
                  }
               };
        }

    }
}
