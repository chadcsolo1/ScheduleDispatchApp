using Jobs.Application.Abstractions;
using Jobs.Application.DTOs;
using Jobs.Application.Mappings;
using Jobs.Domain.Interfaces;
using Jobs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.UpdateJob
{
    public sealed class UpdateJobCommandHandler : ICommandHandler<UpdateJobCommand, JobDto>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<JobDto> Handle(UpdateJobCommand command, CancellationToken cancellationToken)
        {
            // Retrieve the existing job from the repository
            var existingJob = await _jobRepository.GetByIdAsync(command.JobId, cancellationToken);

            //Null check for existing job
            if (existingJob == null)
            {
                throw new InvalidOperationException($"Job with ID {command.JobId} not found.");
            }

            // Create value objects from command data
            var location = new Location(
                command.AddressLine1,
                command.AddressLine2,
                command.City,
                command.State,
                command.ZipCode
            );

            var jobType = new JobType(
                command.JobTypeName,
                command.JobTypeCategory,
                command.JobTypeEstimatedDuration
            );

             var requiredSkills = command.RequiredSkills?
            .Select(skillName => new Skill(skillName))
            .ToList();


            // Update the job properties based on the command
            existingJob.UpdateDetails(command.Description, location, jobType, requiredSkills);
            await _jobRepository.UpdateAsync(existingJob, cancellationToken);

            // Save the changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return the updated job as a JobDto
           return JobMappings.ToDto(existingJob);
        }
       
    }
}

