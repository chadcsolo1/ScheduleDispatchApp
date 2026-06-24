using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure
{
    internal class JobsDbContext : DbContext
    {
        public JobsDbContext(DbContextOptions<JobsDbContext> options)
            : base(options)
        {
            
        }

            public DbSet<Job> Jobs => Set<Job>();
            public DbSet<ChecklistItem> CheckListItems => Set<ChecklistItem>();
            public DbSet<Attachment> Attachments => Set<Attachment>();

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobsDbContext).Assembly);

                base.OnModelCreating(modelBuilder);
            }
    }
}
