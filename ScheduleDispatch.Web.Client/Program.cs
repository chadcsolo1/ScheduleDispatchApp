using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScheduleDispatch.UI.Services.Jobs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001")
});

builder.Services.AddScoped<JobsService>();

await builder.Build().RunAsync();
