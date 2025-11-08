using Microsoft.EntityFrameworkCore;
using WorkflowTrackingSystem.API.Middlewares;
using WorkflowTrackingSystem.Application.Interfaces;
using WorkflowTrackingSystem.Application.Interfaces.Services;
using WorkflowTrackingSystem.Application.Services;
using WorkflowTrackingSystem.Domain.Interfaces;
using WorkflowTrackingSystem.Infrastructure.Persistence;

using WorkflowTrackingSystem.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);
// Add EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IStepValidationService, StepValidationService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
var app = builder.Build();
app.UseMiddleware<StepValidationMiddleware>();//use middleware
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
