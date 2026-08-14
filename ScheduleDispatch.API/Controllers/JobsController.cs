using Jobs.Application.Abstractions;
using Jobs.Application.Commands.CreateJob;
using Jobs.Application.Commands.DeleteJob;
using Jobs.Application.Commands.UpdateJob;
using Jobs.Application.DTOs;
using Jobs.Application.Queries.GetAllJobs;
using Jobs.Application.Queries.GetJobById;
using Jobs.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

            //return CreatedAtAction(nameof(GetJobByIdQuery), new { id = result.Id }, response);
            return Created($"/api/jobs/{result.Id}", response);
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
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                JobTypeName = dto.JobTypeName,
                JobTypeCategory = dto.JobTypeCategory,
                JobTypeEstimatedDuration = dto.JobTypeEstimatedDuration,
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
        public async Task<ActionResult<IEnumerable<JobResponse>>> GetJobs([FromQuery (Name = "q")] string? searchTerm,
            CancellationToken cancellationToken)
        {
            var query = new GetAllJobsQuery(searchTerm);

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
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                JobTypeName = dto.JobTypeName,
                JobTypeCategory = dto.JobTypeCategory,
                JobTypeEstimatedDuration = dto.JobTypeEstimatedDuration,
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
        // PATCH JOB
        // ------------------------------------------------------------
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<JobResponse>> PatchJob(
            Guid id,
            JsonPatchDocument<UpdateJobRequest> patchDocument,
            CancellationToken cancellationToken)
        {
            // 1. Get the existing job
            var query = new GetJobByIdQuery(id);
            var existingJob = await _queryDispatcher
                .DispatchAsync<GetJobByIdQuery, JobDto>(query, cancellationToken);

            if (existingJob is null)
                return NotFound();

            // 2. Map to UpdateJobRequest
            var request = new UpdateJobRequest
            {
                Description = existingJob.Description,
                AddressLine1 = existingJob.AddressLine1,
                AddressLine2 = existingJob.AddressLine2,
                City = existingJob.City,
                State = existingJob.State,
                ZipCode = (int)existingJob.ZipCode,
                JobTypeName = existingJob.JobTypeName,
                JobTypeCategory = existingJob.JobTypeCategory,
                JobTypeEstimatedDuration = (TimeSpan)existingJob.JobTypeEstimatedDuration,
                RequiredSkills = (List<string>)existingJob.RequiredSkills
            };

            // 3. Apply the patch document
            patchDocument.ApplyTo(request, ModelState);

            // 4. Validate the patched model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 5. Use existing UpdateJobCommand
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
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                JobTypeName = dto.JobTypeName,
                JobTypeCategory = dto.JobTypeCategory,
                JobTypeEstimatedDuration = dto.JobTypeEstimatedDuration,
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
