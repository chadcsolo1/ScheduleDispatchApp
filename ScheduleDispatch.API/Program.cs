using Jobs.Application.Extensions;
using Jobs.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ScheduleDispatch.API.Middleware;
using ScheduleDispatch.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Building in content negotiation to our API controllers. This allows the API to return responses in different formats (e.g., JSON, XML) based on the client's request.
builder.Services.AddControllers(options =>
{
    //options.ReturnHttpNotAcceptable = false; // Return 406 Not Acceptable if the requested format is not supported
});
//.AddNewtonsoftJson()
//.AddXmlSerializerFormatters();

//Custom Exception Handling
//builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Swagger
builder.Services.AddSwaggerGen();

// Register controllers and ensure Newtonsoft.Json formatters are available
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

//Content negotiation and formatters
builder.Services.Configure<MvcOptions>(options =>
{
    NewtonsoftJsonOutputFormatter formatter = options.OutputFormatters
        .OfType<NewtonsoftJsonOutputFormatter>()
        .First();

// Make sure Microsoft.AspNetCore.Mvc.NewtonsoftJson package is referenced in the project file (PackageReference) if not already present.

// If you prefer a safe lookup, replace .First() with .FirstOrDefault() and handle null accordingly.

    formatter.SupportedMediaTypes.Add(CustomMediaTypeNames.Application.HateosJson);
});

// Add custom application services (e.g., LinkService)
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<LinkService>();
// Register Application Layer (Commands, Queries, Dispatchers)
builder.Services.AddJobsApplication();

// Register Infrastructure (DbContext, Repositories, etc.)
builder.Services.AddJobsPersistence(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

//app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
