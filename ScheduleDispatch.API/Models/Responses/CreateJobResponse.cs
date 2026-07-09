namespace ScheduleDispatch.API.Models.Responses
{
    public sealed class CreateJobResponse
    {
        public Guid JobId { get; init; }
        public DateTime CreatedAt { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
