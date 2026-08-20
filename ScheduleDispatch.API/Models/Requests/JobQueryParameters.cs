using Jobs.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleDispatch.API.Models.Requests
{
    public sealed record JobQueryParameters
    {
        [FromQuery (Name = "q")]
        public string? SearchTerm { get; init; }
        public JobType? JobType { get; init; }
        public Location? Location { get; init; }
    }
}
