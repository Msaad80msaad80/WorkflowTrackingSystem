using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.Interfaces
{
    public interface IProcessService
    {
        Task<ProcessDto> StartProcessAsync(Guid workflowId);
        Task<ProcessDto> ExecuteStepAsync(Guid processId, Guid stepId, ActionType action);
        Task<ProcessDto?> GetProcessByIdAsync(Guid processId);
        Task<IEnumerable<ProcessDto>> GetProcessesAsync(ProcessStatus? status = null);
    }
}
