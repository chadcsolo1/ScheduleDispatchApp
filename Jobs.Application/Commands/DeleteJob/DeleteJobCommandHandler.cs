using Jobs.Application.Abstractions;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.DeleteJob
{
    public sealed class DeleteJobCommandHandler : ICommandHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteJobCommand command, CancellationToken cancellationToken)
        {
            var existingJob = await _jobRepository.GetByIdAsync(command.JobId, cancellationToken);

            if (existingJob == null)
            {
                throw new InvalidOperationException($"Job with ID {command.JobId} not found.");
            }

            await _jobRepository.DeleteAsync(existingJob, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
