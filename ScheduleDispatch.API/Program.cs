using Jobs.Application.Extensions;
using Jobs.Infrastructure.Extensions;
using ScheduleDispatch.API.Middleware;

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
builder.Services.AddEndpointsApiExplorer();

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
