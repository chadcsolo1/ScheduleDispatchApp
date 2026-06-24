using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Persistence.Configurations
{
    public sealed class CheckListItemConfiguration : IEntityTypeConfiguration<ChecklistItem>
    {
            public void Configure(EntityTypeBuilder<ChecklistItem> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.IsRequired)
            .IsRequired();

        builder.Property(c => c.IsCompleted)
            .IsRequired();
    }
    }
}
