using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Application.Interfaces.Services;

namespace WorkflowTrackingSystem.Application.Services
{
    public class StepValidationService : IStepValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StepValidationService> _logger;

        public StepValidationService(HttpClient httpClient, ILogger<StepValidationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public bool RequiresValidation(string stepName)
        {
            // Simulate logic: Only "Finance Approval" requires validation
            return stepName.Equals("Finance Approval", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> ValidateStepAsync(string stepName, ExecuteStepDto dto)
        {
            try
            {
                _logger.LogInformation("Simulating external validation for step {StepName}", stepName);

                // Simulate API call to external system (you can replace with actual API later)
                await Task.Delay(500); // Simulate latency

                // Fake rule: if ActionType == Reject → validation fails
                if (dto.ActionType.ToString().Equals("Reject", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("External validation failed for step {StepName}", stepName);
                    return false;
                }

                _logger.LogInformation("Validation passed for step {StepName}", stepName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validation error occurred for step {StepName}", stepName);
                return false;
            }
        }
    }
}
