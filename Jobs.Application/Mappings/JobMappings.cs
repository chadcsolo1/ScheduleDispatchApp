using Jobs.Application.DTOs;
using Jobs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Mappings
{
    public static class JobMappings
    {
        public static JobDto ToDto(Job job)
        {
            return new JobDto
            {
                Id = job.JobId,
                CustomerId = job.CustomerId,
                Description = job.Description,
                Status = job.Status.ToString(),
                CreatedAt = job.CreatedAt,
                ScheduledFor = job.ScheduledFor,
                AssignedTechnicianId = job.AssignedTechnicianId,

                Checklist = job.Checklist
                    .Select(ToDto)
                    .ToList(),

                Attachments = job.Attachments
                    .Select(ToDto)
                    .ToList(),

                RequiredSkills = job.RequiredSkills
                    .Select(s => s.Name) // assuming Skill VO has Name
                    .ToList()
            };
        }

        public static ChecklistItemDto ToDto(ChecklistItem item)
        {
            return new ChecklistItemDto
            {
                Id = item.ChecklistItemId,
                Description = item.Description,
                IsRequired = item.IsRequired,
                IsCompleted = item.IsCompleted,
                CompletedAt = item.CompletedAt
            };
        }

        public static AttachmentDto ToDto(Attachment attachment)
        {
            return new AttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                Url = attachment.Url,
                UploadedAt = attachment.UploadedAt,
                IsDeleted = attachment.IsDeleted,
                DeletedAt = attachment.DeletedAt
            };
        }
    }
}
