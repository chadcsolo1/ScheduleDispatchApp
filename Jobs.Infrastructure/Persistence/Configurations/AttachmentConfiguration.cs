using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Persistence.Configurations
{
    public sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(t => t.Id);

         // Properties
        builder.Property(a => a.FileName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Url)
            .HasMaxLength(2048) // URLs can be long
            .IsRequired();

        builder.Property(a => a.UploadedAt)
            .IsRequired();

        builder.Property(a => a.IsDeleted)
            .IsRequired();

        builder.Property(a => a.DeletedAt)
            .IsRequired(false);

        // Optional: Global query filter for soft delete
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
    }
}
