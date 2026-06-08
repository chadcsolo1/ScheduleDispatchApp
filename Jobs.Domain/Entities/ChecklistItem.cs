using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class ChecklistItem
    {
        public ChecklistItem(Guid guid, string description, bool isRequired, bool isCompleted, DateTime? completedAt)
        {
            Guid = guid;
            Description = description;
            IsRequired = isRequired;
            IsCompleted = isCompleted;
            CompletedAt = completedAt;
        }

        public Guid ChecklistItemId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public bool IsRequired { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public Guid Guid { get; }

        public ChecklistItem(Guid id, string description, bool isRequired)
        {
            ChecklistItemId = id;
            Description = description;
            IsRequired = isRequired;
        }

        public void MarkCompleted()
        {
            if (IsCompleted)
                return;

            IsCompleted = true;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
