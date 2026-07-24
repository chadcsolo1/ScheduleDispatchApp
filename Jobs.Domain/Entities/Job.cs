using Jobs.Domain.Enums;
using Jobs.Domain.Events;
using Jobs.Domain.Exceptions;
using Jobs.Domain.ValueObjects;
using SharedKernel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class Job : AggregateRoot
    {
        public Guid JobId { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? ScheduledFor { get; private set; }
        public Guid AssignedTechnicianId { get; private set; }
        public Location? Location { get; private set; }
        public JobType? JobType { get; private set; }
        public List<Skill>? RequiredSkills { get; private set; } = new();
        public List<ChecklistItem>? Checklist { get; private set; } = new();
        public List<Attachment>? Attachments { get; private set; } = new();
        public JobStatus Status { get; private set; }

        private Job() { } // EF Core

        public Job(Guid customerId, Location location, JobType jobType, string description)
        {
            JobId = Guid.NewGuid();
            CustomerId = customerId;
            Location = location;
            JobType = jobType;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            Status = JobStatus.Created;

            AddDomainEvent(new JobCreatedEvent(JobId));
        }

        // ------------------------------------------------------------
        // Assign Technician
        // ------------------------------------------------------------
        public void AssignTechnician(Guid technicianId)
        {
            if (Status == JobStatus.Completed || Status == JobStatus.Canceled)
                throw new InvalidJobOperationException("Cannot assign a technician to a completed or canceled job.");

            AssignedTechnicianId = technicianId;
            Status = JobStatus.Assigned;

            AddDomainEvent(new JobAssignedEvent(JobId, technicianId));
        }

        // ------------------------------------------------------------
        // Schedule Job
        // ------------------------------------------------------------
        public void Schedule(DateTime scheduledFor)
        {
            if (scheduledFor < DateTime.UtcNow)
                throw new InvalidJobOperationException("Cannot schedule a job in the past.");

            if (Status == JobStatus.Completed || Status == JobStatus.Canceled)
                throw new InvalidJobOperationException("Cannot schedule a completed or canceled job.");

            ScheduledFor = scheduledFor;
            Status = JobStatus.Scheduled;

            AddDomainEvent(new JobScheduledEvent(JobId, scheduledFor));
        }

        // ------------------------------------------------------------
        // Add Attachment
        // ------------------------------------------------------------
        public void AddAttachment(string fileName, string url)
        {
            var attachment = new Attachment(Guid.NewGuid(), fileName, url, DateTime.UtcNow);
            Attachments.Add(attachment);

            // Optional: domain event for notifications or auditing
            //AddDomainEvent(new AttachmentAddedEvent(JobId, attachment.Id));
        }

        // ------------------------------------------------------------
        // Remove Attachment
        // ------------------------------------------------------------
        public void RemoveAttachment(Guid attachmentId)
        {
            var attachment = Attachments.FirstOrDefault(a => a.Id == attachmentId)
                ?? throw new InvalidJobOperationException("Attachment not found.");

            attachment.Delete();

            // Optional domain event
            // AddDomainEvent(new AttachmentRemovedEvent(Id, attachmentId));
        }

        // ------------------------------------------------------------
        // Replace Attachment
        // ------------------------------------------------------------
        public void ReplaceAttachment(Guid attachmentId, string newFileName, string newUrl)
        {
            var attachment = Attachments.FirstOrDefault(a => a.Id == attachmentId)
                ?? throw new InvalidJobOperationException("Attachment not found.");

            attachment.Replace(newFileName, newUrl);

            // Optional domain event
            // AddDomainEvent(new AttachmentReplacedEvent(Id, attachmentId));
        }

        // ------------------------------------------------------------
        // Add Checklist Item
        // ------------------------------------------------------------
        public void AddChecklistItem(string description, bool isRequired = false)
        {
            var item = new ChecklistItem(Guid.NewGuid(), description, isRequired);
            Checklist.Add(item);
        }

        // ------------------------------------------------------------
        // Complete Checklist Item
        // ------------------------------------------------------------
        public void CompleteChecklistItem(Guid itemId)
        {
            var item = Checklist.FirstOrDefault(x => x.ChecklistItemId == itemId)
                ?? throw new InvalidJobOperationException("Checklist item not found.");

            item.MarkCompleted();
        }

        // ------------------------------------------------------------
        // Mark Job Completed
        // ------------------------------------------------------------
        public void MarkCompleted()
        {
            if (Status == JobStatus.Canceled)
                throw new InvalidJobOperationException("Cannot complete a canceled job.");

            if (Checklist.Any(x => x.IsRequired && (bool)!x.IsCompleted))
                throw new InvalidJobOperationException("Cannot complete job until all required checklist items are completed.");


            Status = JobStatus.Completed;

            AddDomainEvent(new JobCompletedEvent(JobId));
        }

        // ------------------------------------------------------------
        // Cancel Job
        // ------------------------------------------------------------
        public void MarkCanceled(string reason)
        {
            if (Status == JobStatus.Completed)
                throw new InvalidJobOperationException("Cannot cancel a completed job.");

            Status = JobStatus.Canceled;

            AddDomainEvent(new JobCanceledEvent(JobId, reason));
        }




    }
}
