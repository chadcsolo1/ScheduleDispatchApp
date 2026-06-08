using Jobs.Domain.Enums;
using Jobs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class Job
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

            //AddDomainEvent(new JobCreatedEvent(Id));
        }

        // ------------------------------------------------------------
        // Assign Technician
        // ------------------------------------------------------------
        public void AssignTechnician(Guid technicianId)
        {
            if (Status == JobStatus.Completed || Status == JobStatus.Canceled)
                throw new Exception("Cannot assign a technician to a completed or canceled job.");

            AssignedTechnicianId = technicianId;
            Status = JobStatus.Assigned;

            //AddDomainEvent(new JobAssignedEvent(Id, technicianId));
        }

        // ------------------------------------------------------------
        // Schedule Job
        // ------------------------------------------------------------
        public void Schedule(DateTime scheduledFor)
        {
            if (scheduledFor < DateTime.UtcNow)
                throw new Exception("Cannot schedule a job in the past.");

            if (Status == JobStatus.Completed || Status == JobStatus.Canceled)
                throw new Exception("Cannot schedule a completed or canceled job.");

            ScheduledFor = scheduledFor;
            Status = JobStatus.Scheduled;

            //AddDomainEvent(new JobScheduledEvent(Id, scheduledFor));
        }

        // ------------------------------------------------------------
        // Add Attachment
        // ------------------------------------------------------------
        public void AddAttachment(string fileName, string url)
        {
            var attachment = new Attachment(Guid.NewGuid(), fileName, url, DateTime.UtcNow);
            Attachments.Add(attachment);

            // Optional: domain event for notifications or auditing
            // AddDomainEvent(new AttachmentAddedEvent(Id, attachment.Id));
        }

        // ------------------------------------------------------------
        // Add Checklist Item
        // ------------------------------------------------------------
        public void AddChecklistItem(string description, bool isRequired = false)
        {
            var item = new ChecklistItem(Guid.NewGuid(), description, isRequired, null, null);
            Checklist.Add(item);
        }

        // ------------------------------------------------------------
        // Complete Checklist Item
        // ------------------------------------------------------------
        public void CompleteChecklistItem(Guid itemId)
        {
            var item = Checklist.FirstOrDefault(x => x.Id == itemId)
                ?? throw new Exception("Checklist item not found.");

            item.MarkCompleted();
        }

        // ------------------------------------------------------------
        // Mark Job Completed
        // ------------------------------------------------------------
        public void MarkCompleted()
        {
            if (Status == JobStatus.Canceled)
                throw new Exception("Cannot complete a canceled job.");

            if (Checklist.Any(x => x.IsRequired && !x.IsCompleted))
                throw new Exception("Cannot complete job until all required checklist items are completed.");

            Status = JobStatus.Completed;

            //AddDomainEvent(new JobCompletedEvent(Id));
        }

        // ------------------------------------------------------------
        // Cancel Job
        // ------------------------------------------------------------
        public void MarkCanceled(string reason)
        {
            if (Status == JobStatus.Completed)
                throw new Exception("Cannot cancel a completed job.");

            Status = JobStatus.Canceled;

            //AddDomainEvent(new JobCanceledEvent(Id, reason));
        }

    }
}
