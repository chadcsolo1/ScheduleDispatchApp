using Jobs.Application.Abstractions;
using Jobs.Application.Commands.CreateJob;
using Jobs.Application.Commands.DeleteJob;
using Jobs.Application.Commands.UpdateJob;
using Jobs.Application.DTOs;
using Jobs.Application.Queries.GetAllJobs;
using Jobs.Application.Queries.GetJobById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleDispatch.API.Models.Requests;
using ScheduleDispatch.API.Models.Responses;

namespace ScheduleDispatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public JobsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

                // ------------------------------------------------------------
        // CREATE JOB
        // ------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<CreateJobResponse>> CreateJob(
            [FromBody] CreateJobRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateJobCommand(
                request.CustomerId,
                request.Description,
                request.AddressLine1,
                request.AddressLine2 ?? string.Empty,
                request.City,
                request.State,
                request.ZipCode,
                request.JobTypeName,
                request.JobTypeCategory,
                request.JobTypeEstimatedDuration,
                request.RequiredSkills);

            var result = await _commandDispatcher
                .DispatchAsync<CreateJobCommand, JobDto>(command, cancellationToken);

            var response = new CreateJobResponse
            {
                JobId = result.Id,
                CreatedAt = result.CreatedAt,
                Status = result.Status
            };

            return CreatedAtAction(nameof(GetJobByIdQuery), new { id = result.Id }, response);
        }

        // ------------------------------------------------------------
        // GET JOB BY ID
        // ------------------------------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JobResponse>> GetJobById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetJobByIdQuery(id);

            var dto = await _queryDispatcher
                .DispatchAsync<GetJobByIdQuery, JobDto>(query, cancellationToken);

            if (dto is null)
                return NotFound();

            var response = new JobResponse
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                ScheduledFor = dto.ScheduledFor,
                AssignedTechnicianId = dto.AssignedTechnicianId,
                CheckList = dto.Checklist,
                Attachments = dto.Attachments,
                RequiredSkills = dto.RequiredSkills
            };

            return Ok(response);
        }

         // ------------------------------------------------------------
        // GET ALL JOBS
        // ------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobResponse>>> GetJobs(
            CancellationToken cancellationToken)
        {
            var query = new GetAllJobsQuery();

            var dtos = await _queryDispatcher
                .DispatchAsync<GetAllJobsQuery, IEnumerable<JobDto>>(query, cancellationToken);

            var response = dtos.Select(dto => new JobResponse
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                ScheduledFor = dto.ScheduledFor,
                AssignedTechnicianId = dto.AssignedTechnicianId,
                CheckList = dto.Checklist,
                Attachments = dto.Attachments,
                RequiredSkills = dto.RequiredSkills
            });

            return Ok(response);
        }

                // ------------------------------------------------------------
        // UPDATE JOB
        // ------------------------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<JobResponse>> UpdateJob(
            Guid id,
            [FromBody] UpdateJobRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateJobCommand(
                id,
                request.Description,
                request.AddressLine1,
                request.AddressLine2 ?? string.Empty,
                request.City,
                request.State,
                request.ZipCode,
                request.JobTypeName,
                request.JobTypeCategory,
                request.JobTypeEstimatedDuration,
                request.RequiredSkills);

            var dto = await _commandDispatcher
                .DispatchAsync<UpdateJobCommand, JobDto>(command, cancellationToken);

            var response = new JobResponse
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                Description = dto.Description,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt,
                ScheduledFor = dto.ScheduledFor,
                AssignedTechnicianId = dto.AssignedTechnicianId,
                CheckList = dto.Checklist,
                Attachments = dto.Attachments,
                RequiredSkills = dto.RequiredSkills
            };

            return Ok(response);
        }

        // ------------------------------------------------------------
        // DELETE JOB
        // ------------------------------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteJob(
            Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteJobCommand(id);

            await _commandDispatcher
                .DispatchAsync<DeleteJobCommand, bool>(command, cancellationToken);

            return NoContent();
        }
    }
}
