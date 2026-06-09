using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Entities;
using Jobs.Domain.Interfaces;
using Jobs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.CreateJob
{
    public sealed class CreateJobCommandHandler
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<JobDto> Handle(CreateJobCommand command, CancellationToken cancellationToken)
        {
            var location = new Location(
                command.AddressLine1,
                command.AddressLine2,
                command.City,
                command.State,
                command.ZipCode);

            var jobType = new JobType(
                command.JobTypeName,
                command.JobTypeCategory,
                command.JobTypeEstimatedDuration);

            var skills = command.RequiredSkills
                .Select(s => new Skill(s))
                .ToList();

            var job = new Job(
                command.CustomerId,
                location,
                jobType,
                command.Description);


            await _jobRepository.AddAsync(job, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return JobMappings.ToDto(job);
        }
    }
}
