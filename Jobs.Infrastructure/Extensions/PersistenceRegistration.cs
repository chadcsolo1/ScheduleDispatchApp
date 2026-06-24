using Jobs.Application.Abstractions;
using Jobs.Domain.Interfaces;
using Jobs.Infrastructure.Persistence;
using Jobs.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Extensions
{
    public static class PersistenceRegistration
    {
            public static IServiceCollection AddJobsPersistence(
                this IServiceCollection services,
                IConfiguration configuration)
            {
                services.AddDbContext<JobsDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("JobsConnection")));

                services.AddScoped<IJobRepository, JobRepository>();
                services.AddScoped<IJobReadRepository, JobReadRepository>();
                services.AddScoped<IUnitOfWork, EfUnitOfWork>();

                return services;
            }
    }
}
