using Jobs.Application.Abstractions;
using Jobs.Domain.Exceptions;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.ScheduleJob
{
    public sealed class ScheduleJobCommandHandler
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ScheduleJobCommand command, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(command.JobId, cancellationToken)
                ?? throw new InvalidJobOperationException("Job not found.");

            job.Schedule(command.ScheduledFor);

            await _jobRepository.UpdateAsync(job, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
