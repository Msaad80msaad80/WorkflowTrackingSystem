using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.Interfaces;
using WorkflowTrackingSystem.Application.DTOs;
using System;
using WorkflowTrackingSystem.Application.Interfaces.Services;


namespace WorkflowTrackingSystem.API.Middlewares
{
    public class StepValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StepValidationMiddleware> _logger;

        public StepValidationMiddleware(RequestDelegate next, ILogger<StepValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IStepValidationService validationService)
        {
            // Only apply validation logic for process execution endpoint
            if (context.Request.Path.StartsWithSegments("/v1/processes/execute") &&
                context.Request.Method == HttpMethods.Post)
            {
                try
                {
                    context.Request.EnableBuffering();

                    // Read request body
                    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        // Deserialize request to ProcessExecutionDto
                        var executionDto = JsonSerializer.Deserialize<ProcessExecutionDto>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (executionDto != null)
                        {
                            // Check if this step requires validation
                            bool requiresValidation = validationService.RequiresValidation(executionDto.StepName);

                            if (requiresValidation)
                            {
                                _logger.LogInformation("Validation required for step {StepName}", executionDto.StepName);

                                var executeDto = new ExecuteStepDto
                                {
                                    ProcessId = executionDto.ProcessId,

                                    StepName = executionDto.StepName,
                                    
                                };

                                bool validationPassed = await validationService.ValidateStepAsync(executionDto.StepName, executeDto);


                                if (!validationPassed)
                                {
                                    _logger.LogWarning("Step validation failed for {StepName}", executionDto.StepName);

                                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                    await context.Response.WriteAsJsonAsync(new
                                    {
                                        success = false,
                                        message = $"Validation failed for step '{executionDto.StepName}'."
                                    });
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during step validation middleware.");

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = "Internal server error during validation."
                    });
                    return;
                }
            }

            // ✅ Proceed to next middleware or controller
            await _next(context);
        }
    }
}
