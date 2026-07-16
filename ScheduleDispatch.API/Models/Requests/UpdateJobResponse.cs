namespace ScheduleDispatch.API.Models.Requests
{
    public sealed class UpdateJobResponse
    {
        public string Description { get; init; } = string.Empty;

        // Location
        public string AddressLine1 { get; init; } = string.Empty;
        public string? AddressLine2 { get; init; }
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public int ZipCode { get; init; }

        // Job Type
        public string JobTypeName { get; init; } = string.Empty;
        public string JobTypeCategory { get; init; } = string.Empty;
        public TimeSpan JobTypeEstimatedDuration { get; init; }

        // Skills
        public List<string> RequiredSkills { get; init; } = new();
    }
}
