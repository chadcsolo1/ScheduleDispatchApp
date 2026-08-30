

using Jobs.Domain.Interfaces;

namespace Jobs.Application.Queries.GetAllJobs
{
    public sealed record GetAllJobsQuery : IJobQuerySpecification
    {
        public string? SearchTerm { get; init; } = null;

        public string? JobTypeName { get; init; } = null;

        public string? JobTypeCategory { get; init; } = null;

        public string? City { get; init; } = null;

        public string? State { get; init; } = null;

        public int? ZipCode { get; init; } = null;

        public string? SortBy { get; init; } = null;

        public string? SortDirection { get; init; } = "asc";

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 10;
    }
}
