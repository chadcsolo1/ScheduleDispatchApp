using Jobs.Application.Abstractions;
using Jobs.Application.Commands.AddAttachment;
using Jobs.Application.Commands.AssignTechnician;
using Jobs.Application.Commands.CancelJob;
using Jobs.Application.Commands.CompleteJob;
using Jobs.Application.Commands.CreateJob;
using Jobs.Application.Commands.DeleteJob;
using Jobs.Application.Commands.ScheduleJob;
using Jobs.Application.Commands.UpdateJob;
using Jobs.Application.Dispatching;
using Jobs.Application.DTOs;
using Jobs.Application.Queries.GetAllJobs;
using Jobs.Application.Queries.GetJobById;
using Jobs.Application.Queries.GetJobsByStatus;
using Jobs.Application.Queries.GetJobsForCustomer;
using Jobs.Application.Queries.GetOpenJobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Extensions{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJobsApplication(this IServiceCollection services)
        {
            // Register dispatchers
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            // Register command handlers
            services.AddScoped<ICommandHandler<CreateJobCommand, JobDto>, CreateJobCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateJobCommand, JobDto>, UpdateJobCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteJobCommand, bool>, DeleteJobCommandHandler>();
            //services.AddScoped<ICommandHandler<CancelJobCommand>, CancelJobCommandHandler>();
            //services.AddScoped<ICommandHandler<CompleteJobCommand>, CompleteJobCommandHandler>();
            //services.AddScoped<ICommandHandler<ScheduleJobCommand>, ScheduleJobCommandHandler>();
            //services.AddScoped<ICommandHandler<AssignTechnicianCommand>, AssignTechnicianCommandHandler>();
            //services.AddScoped<ICommandHandler<AddAttachmentCommand>, AddAttachmentCommandHandler>();


            // Register query handlers
            services.AddScoped<IQueryHandler<GetAllJobsQuery, IEnumerable<JobDto>>, GetAllJobsQueryHandler>();
            services.AddScoped<IQueryHandler<GetJobByIdQuery, JobDto?>, GetJobByIdQueryHandler>();
            services.AddScoped<IQueryHandler<GetJobsByStatusQuery, IReadOnlyList<JobDto>>, GetJobsByStatusQueryHandler>();
            services.AddScoped<IQueryHandler<GetJobsForCustomerQuery, IReadOnlyList<JobDto>>, GetJobsForCustomerQueryHandler>();
            services.AddScoped<IQueryHandler<GetOpenJobsQuery, IReadOnlyList<JobDto>>, GetOpenJobsQueryHandler>();


            //// Register all command handlers
            //services.Scan(scan => scan
            //    .FromAssemblyOf<ICommandHandler>()
            //    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            //// Register all query handlers
            //services.Scan(scan => scan
            //    .FromAssemblyOf<IQueryHandler>()
            //    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            return services;
        }
    }
}
