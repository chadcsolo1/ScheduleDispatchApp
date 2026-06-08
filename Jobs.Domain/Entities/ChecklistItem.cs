using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class ChecklistItem
    {
        public Guid ChecklistItemId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
