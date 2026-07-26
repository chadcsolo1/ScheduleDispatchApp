using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScheduleDispatch.UI.Services.Jobs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



//builder.Services.AddHttpClient<JobsService>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7218"); // your API URL
//});

builder.Services.AddScoped<JobsService>();

await builder.Build().RunAsync();
