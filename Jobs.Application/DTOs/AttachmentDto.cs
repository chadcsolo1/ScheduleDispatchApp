using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.DTOs
{
    public sealed class AttachmentDto
    {
        public Guid Id { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public DateTime UploadedAt { get; init; }
        public bool IsDeleted { get; init; }
        public DateTime? DeletedAt { get; init; }
    }
}
