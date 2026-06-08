using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Jobs.Domain.Entities
{
    public class Attachment
    {
        public Guid Id { get; private set; }
        public string FileName { get; private set; }
        public string Url { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private Attachment() { } // EF Core

        public Attachment(Guid id, string fileName, string url, DateTime uploadedAt)
        {
            Id = id;
            FileName = fileName;
            Url = url;
            UploadedAt = uploadedAt;
            IsDeleted = false;
        }



        // ------------------------------------------------------------
        // Mark as Deleted (Soft Delete)
        // ------------------------------------------------------------
        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        // ------------------------------------------------------------
        // Replace the file (e.g., technician re-uploads)
        // ------------------------------------------------------------
        public void Replace(string newFileName, string newUrl)
        {
            FileName = newFileName;
            Url = newUrl;
            UploadedAt = DateTime.UtcNow;
        }


    }
}
