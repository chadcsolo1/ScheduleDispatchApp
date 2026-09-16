using System.Linq;
using Jobs.Domain.Interfaces;

namespace Jobs.Domain.Models
{
    public sealed record PaginationResult<T> : ICollectionResponse<T>, ILinksResponse
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

        public List<LinkDto> Links
        {
            get; set;
        }

        public static PaginationResult<T> Create(List<T> items,
            int page,
            int pageSize,
            int totalCount)
        {
            //int count = source.Count();
            //var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationResult<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

    }
}
