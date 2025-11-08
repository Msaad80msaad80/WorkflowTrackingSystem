using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.DTOs;

namespace WorkflowTrackingSystem.Application.Interfaces.Services
{
    public interface IStepValidationService
    {
        /// <summary>
        /// Checks if a specific workflow step requires validation.
        /// </summary>
        bool RequiresValidation(string stepName);

        /// <summary>
        /// Performs validation for a step (simulate external API call).
        /// </summary>
        Task<bool> ValidateStepAsync(string stepName, ExecuteStepDto dto);
    }
}
