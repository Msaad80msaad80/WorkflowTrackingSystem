using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Application.Interfaces;
using WorkflowTrackingSystem.Application.Interfaces.Repositories;

using WorkflowTrackingSystem.Domain.Entities;
using WorkflowTrackingSystem.Domain.Enums;
using WorkflowTrackingSystem.Domain.Interfaces;

namespace WorkflowTrackingSystem.Application.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly ILogger<ProcessService> _logger;

        public ProcessService(
            IProcessRepository processRepository,
            IWorkflowRepository workflowRepository,
            ILogger<ProcessService> logger)
        {
            _processRepository = processRepository;
            _workflowRepository = workflowRepository;
            _logger = logger;
        }

        public async Task<ProcessDto> StartProcessAsync(Guid workflowId)
        {
            var workflow = await _workflowRepository.GetByIdAsync(workflowId);
            if (workflow == null)
                throw new Exception($"Workflow {workflowId} not found.");

            var process = new Process
            {
                Id = Guid.NewGuid(),
                WorkflowId = workflowId,
                Status = ProcessStatus.Running,
                StartedAt = DateTime.UtcNow,
                Steps = workflow.Steps.Select(s => new ProcessStep
                {
                    Id = Guid.NewGuid(),
                    WorkflowStepId = s.Id,
                    Status = (StepStatus)ProcessStatus.Pending
                }).ToList()
            };

            await _processRepository.AddAsync(process);
            _logger.LogInformation("Process started for workflow {WorkflowId}", workflowId);

            return new ProcessDto
            {
                Id = process.Id,
                WorkflowId = workflowId,
                Status = process.Status,
                StartedAt = process.StartedAt
            };
        }

        public async Task<ProcessDto> ExecuteStepAsync(Guid processId, Guid stepId, ActionType action)
        {
            var process = await _processRepository.GetByIdAsync(processId)
                ?? throw new Exception($"Process {processId} not found.");

            var step = process.Steps.FirstOrDefault(s => s.WorkflowStepId == stepId)
                ?? throw new Exception($"Step {stepId} not found in process {processId}.");
            step.Status = action switch
            {
                ActionType.Approve => StepStatus.Completed,
                ActionType.Reject => StepStatus.Rejected,
                _ => step.Status
            };

            await _processRepository.UpdateAsync(process);
            _logger.LogInformation("Step {StepId} in process {ProcessId} executed as {Action}.", stepId, processId, action);

            return new ProcessDto
            {
                Id = process.Id,
                WorkflowId = process.WorkflowId,
                Status = process.Status,
                Steps = process.Steps.Select(s => new ProcessStepDto
                {
                    Id = s.Id,
                    WorkflowStepId = s.WorkflowStepId,
                    Status = s.Status.ToString(),
                }).ToList()
            };
        }

        public async Task<ProcessDto?> GetProcessByIdAsync(Guid processId)
        {
            var process = await _processRepository.GetByIdAsync(processId);
            if (process == null) return null;

            return new ProcessDto
            {
                Id = process.Id,
                WorkflowId = process.WorkflowId,
                Status = process.Status,
                Steps = process.Steps.Select(s => new ProcessStepDto
                {
                    Id = s.Id,
                    WorkflowStepId = s.WorkflowStepId,
                    Status = s.Status.ToString()
                }).ToList()
            };
        }

        public async Task<IEnumerable<ProcessDto>> GetProcessesAsync(ProcessStatus? status = null)
        {
            var processes = await _processRepository.GetAllAsync(status);

            return processes.Select(p => new ProcessDto
            {
                Id = p.Id,
                WorkflowId = p.WorkflowId,
                Status = p.Status,
                StartedAt = p.StartedAt,
                Steps = p.Steps.Select(s => new ProcessStepDto
                {
                    Id = s.Id,
                    WorkflowStepId = s.WorkflowStepId,
                    Status = s.Status.ToString()
                }).ToList()
            });
        }
    }
}
