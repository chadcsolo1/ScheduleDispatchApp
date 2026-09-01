using ScheduleDispatch.API.DTOs;

namespace ScheduleDispatch.API.Models.Responses
{
    public sealed record PaginationResult<T> : ICollectionResponse<T>
    {
        public List<T> Items { get; init; } = new List<T>();
        public int Page
        {
            get; init;
        }
        public int PageSize
        {
            get;
            set;
        }
        public int TotalCount
        {
            get;
            set;
        }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;

        public static async Task<PaginationResult<T>> CreateAsync(IQueryable<T> source, int page, int pageSize)
        {
            int count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationResult<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            };
        }

    }
}
