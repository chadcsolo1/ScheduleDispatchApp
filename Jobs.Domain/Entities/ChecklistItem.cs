using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class ChecklistItem
    {
        public Guid ChecklistItemId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public bool IsRequired { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        // Parameterless constructor for EF Core
        private ChecklistItem() { }

        public ChecklistItem(Guid checklistItemId, string description, bool isRequired)
        {
            ChecklistItemId = checklistItemId;
            Description = description;
            IsRequired = isRequired;
            IsCompleted = false;
            CompletedAt = null;
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
