using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Persistence.Configurations
{
    public sealed class JobConfiguration : IEntityTypeConfiguration<Job>
    {
                public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(j => j.JobId);

            builder.Property(j => j.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(j => j.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(j => j.ScheduledFor)
                .IsRequired(false);

            builder.OwnsMany(j => j.Checklist, cb =>
            {
                cb.ToTable("JobCheckListItems");
                cb.WithOwner().HasForeignKey("JobId");
                cb.HasKey("Id");
            });

            builder.Navigation(j => j.Checklist).AutoInclude();
        }
    }
}
