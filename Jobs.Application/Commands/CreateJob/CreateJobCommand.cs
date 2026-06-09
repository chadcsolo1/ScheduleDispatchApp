using Jobs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.CreateJob
{
    public sealed record CreateJobCommand(
    Guid CustomerId,
    string Description,
    string AddressLine1,
    string AddressLine2,
    string City,
    string State,
    int ZipCode,
    string JobTypeName,
    string JobTypeCategory,
    TimeSpan JobTypeEstimatedDuration,
    List<string> RequiredSkills);
}
