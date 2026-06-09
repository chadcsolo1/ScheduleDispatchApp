using Jobs.Application.Abstractions;
using Jobs.Domain.Exceptions;
using Jobs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Commands.CancelJob
{
    public sealed class CancelJobCommandHandler
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelJobCommand command, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(command.JobId, cancellationToken)
                ?? throw new InvalidJobOperationException("Job not found.");

            job.MarkCanceled(command.Reason);

            await _jobRepository.UpdateAsync(job, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
