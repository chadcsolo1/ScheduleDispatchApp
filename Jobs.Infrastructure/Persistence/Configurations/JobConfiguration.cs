using Jobs.Domain.Entities;
using Jobs.Domain.ValueObjects;
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

                    // Ignore domain events (not persisted to database)
                    builder.Ignore(j => j.DomainEvents);

                    builder.Property(j => j.Description)
                        .HasMaxLength(500)
                        .IsRequired();

                    builder.Property(j => j.Status)
                        .HasConversion<string>()
                        .IsRequired();

                    builder.Property(j => j.ScheduledFor)
                        .IsRequired(false);

                    // Configure Location value object as owned entity
                    builder.OwnsOne(j => j.Location, loc =>
                    {
                        loc.Property(l => l.AddressLine1).HasMaxLength(200).IsRequired();
                        loc.Property(l => l.AddressLine2).HasMaxLength(200).IsRequired(false);
                        loc.Property(l => l.City).HasMaxLength(100).IsRequired();
                        loc.Property(l => l.State).HasMaxLength(50).IsRequired();
                        loc.Property(l => l.ZipCode).IsRequired();
                    });

                    // Configure JobType value object as owned entity
                    builder.OwnsOne(j => j.JobType, jt =>
                    {
                        jt.Property(t => t.JobTypeName).HasMaxLength(100).IsRequired();
                        jt.Property(t => t.JobTypeCategory).HasMaxLength(50).IsRequired();
                        jt.Property(t => t.JobTypeEstimatedDuration).IsRequired();
                    });

                    // Configure RequiredSkills collection as owned entities
                    builder.OwnsMany(j => j.RequiredSkills, skill =>
                    {
                        skill.ToTable("JobRequiredSkills");
                        skill.WithOwner().HasForeignKey("JobId");
                        skill.Property<int>("Id"); // Shadow property for EF Core
                        skill.HasKey("Id");
                        skill.Property(s => s.Name).HasMaxLength(100).IsRequired();
                    });

                        // Configure Checklist as owned entities
                        builder.OwnsMany(j => j.Checklist, cb =>
                        {
                            cb.ToTable("JobCheckListItems");
                            cb.WithOwner().HasForeignKey("JobId");
                            cb.HasKey(c => c.ChecklistItemId);

                            // ChecklistItem property configurations
                            cb.Property(c => c.Description)
                                .HasMaxLength(200)
                                .IsRequired();

                            cb.Property(c => c.IsRequired)
                                .IsRequired();

                            cb.Property(c => c.IsCompleted)
                                .IsRequired();

                            cb.Property(c => c.CompletedAt)
                                .IsRequired(false);
                        });

                        // Configure relationship with Attachments (independent entity)
                        builder.HasMany(j => j.Attachments)
                            .WithOne()
                            .HasForeignKey("JobId")
                            .OnDelete(DeleteBehavior.Cascade);

                        builder.Navigation(j => j.Checklist).AutoInclude();
                    }
    }
}
