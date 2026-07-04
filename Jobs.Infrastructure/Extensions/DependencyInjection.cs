using Jobs.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Jobs.Infrastructure.Services.FileStorage;

namespace Jobs.Infrastructure.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddJobsInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment env)
        {
            services.AddDbContext<JobsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("JobsDb")));

            if (env.IsDevelopment())
            {
                services.AddScoped<IFileStorageService, LocalFileStorageService>();
            } else
            {
                //services.AddScoped<IFileStorageService, AzureBlobFileStorageService>();
            }

            return services;
        }



    }
}
