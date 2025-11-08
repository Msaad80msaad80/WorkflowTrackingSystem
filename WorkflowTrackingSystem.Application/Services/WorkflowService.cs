using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Application.Interfaces;
using WorkflowTrackingSystem.Domain.Entities;
using WorkflowTrackingSystem.Domain.Enums;
using WorkflowTrackingSystem.Domain.Interfaces;

namespace WorkflowTrackingSystem.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly ILogger<WorkflowService> _logger;

        public WorkflowService(IWorkflowRepository workflowRepository, ILogger<WorkflowService> logger)
        {
            _workflowRepository = workflowRepository;
            _logger = logger;
        }

        public async Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowDto request)
        {
            var workflow = new Workflow
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Steps = request.Steps.Select(s => new WorkflowStep
                {
                    Id = Guid.NewGuid(),
                    StepName = s.StepName,
                    AssignedTo = s.AssignedTo,
                    ActionType = s.ActionType,
                    //NextStep = s.NextStep,
                    RequiresValidation = s.RequiresValidation
                }).ToList(),
                CreatedAt = DateTime.UtcNow
            };

            await _workflowRepository.AddAsync(workflow);
            _logger.LogInformation("Workflow '{Name}' created successfully.", workflow.Name);

            return new WorkflowDto
            {
                Id = workflow.Id,
                Name = workflow.Name,
                Description = workflow.Description,
                Steps = workflow.Steps.Select(s => new WorkflowStepDto
                {
                    Id = s.Id,
                    StepName = s.StepName,
                    AssignedTo = s.AssignedTo,
                    ActionType = s.ActionType,
                    RequiresValidation = s.RequiresValidation
                }).ToList()
            };
        }

        public async Task<IEnumerable<WorkflowDto>> GetAllAsync()
        {
            var workflows = await _workflowRepository.GetAllAsync();

            return workflows.Select(w => new WorkflowDto
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                Steps = w.Steps.Select(s => new WorkflowStepDto
                {
                    Id = s.Id,
                    StepName = s.StepName,
                    AssignedTo = s.AssignedTo,
                    ActionType = s.ActionType,
                    RequiresValidation = s.RequiresValidation
                }).ToList()
            });
        }

        public async Task<WorkflowDto?> GetByIdAsync(Guid id)
        {
            var workflow = await _workflowRepository.GetByIdAsync(id);
            if (workflow == null) return null;

            return new WorkflowDto
            {
                Id = workflow.Id,
                Name = workflow.Name,
                Description = workflow.Description,
                Steps = workflow.Steps.Select(s => new WorkflowStepDto
                {
                    Id = s.Id,
                    StepName = s.StepName,
                    AssignedTo = s.AssignedTo,
                    ActionType = s.ActionType,
                    RequiresValidation = s.RequiresValidation
                }).ToList()
            };
        }

        //  Update existing workflow
        public async Task<WorkflowDto?> UpdateAsync(Guid id, UpdateWorkflowDto request)
        {
            var workflow = await _workflowRepository.GetByIdAsync(id);
            if (workflow == null)
            {
                _logger.LogWarning("Workflow {Id} not found for update.", id);
                return null;
            }

            workflow.Name = request.Name;
            workflow.Description = request.Description;

            // Handle steps (update existing, add new)
            foreach (var stepDto in request.Steps)
            {
                var existingStep = workflow.Steps.FirstOrDefault(s => s.Id == stepDto.Id);

                if (existingStep != null)
                {
                    // Update existing step
                    existingStep.StepName = stepDto.StepName.ToString();
                    existingStep.AssignedTo = stepDto.AssignedTo;
                    existingStep.ActionType = stepDto.ActionType.ToString();
                    existingStep.RequiresValidation = stepDto.RequiresValidation;
                   // existingStep.NextStep = stepDto.NextStep.ToString();
                }
                else
                {
                    // Add new step
                    workflow.Steps.Add(new WorkflowStep
                    {
                        Id = Guid.NewGuid(),
                        StepName = stepDto.StepName,
                        AssignedTo = stepDto.AssignedTo,
                        ActionType = stepDto.ActionType.ToString(),
                        RequiresValidation = stepDto.RequiresValidation,
                       // NextStep = stepDto.NextStep.ToString()
                    });
                }
            }

            await _workflowRepository.UpdateAsync(workflow);
            _logger.LogInformation("Workflow {Id} updated successfully.", id);

            return new WorkflowDto
            {
                Id = workflow.Id,
                Name = workflow.Name,
                Description = workflow.Description,
                Steps = workflow.Steps.Select(s => new WorkflowStepDto
                {
                    Id = s.Id,
                    StepName = s.StepName,
                    AssignedTo = s.AssignedTo,
                    ActionType = s.ActionType,
                    RequiresValidation = s.RequiresValidation
                }).ToList()
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var workflow = await _workflowRepository.GetByIdAsync(id);
            if (workflow == null)
            {
                _logger.LogWarning("Workflow {Id} not found for deletion.", id);
                return false;
            }

            // call repository method that accepts Guid
            await _workflowRepository.DeleteAsync(id);

            _logger.LogInformation("Workflow {Id} deleted successfully.", id);
            return true;
        }
    }
}
