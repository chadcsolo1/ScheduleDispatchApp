using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class Attachment
    {
        public Guid AttachmentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}
