namespace MovieApi.Core.Paging
{
    public class PagingParameters
    {
        private const int MaxPageSize = 100;

        public int CurrentPage { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = Math.Min(value, MaxPageSize);
        }
    }
}
