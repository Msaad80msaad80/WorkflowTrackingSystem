using Microsoft.EntityFrameworkCore;
using WorkflowTrackingSystem.Domain.Entities;
using WorkflowTrackingSystem.Domain.Interfaces;
using WorkflowTrackingSystem.Infrastructure.Persistence;

namespace WorkflowTrackingSystem.Infrastructure.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly AppDbContext _context;

        public WorkflowRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new workflow (including its steps) to the database.
        /// </summary>
        public async Task AddAsync(Workflow workflow)
        {
            // Ensure steps are attached properly
            if (workflow.Steps != null && workflow.Steps.Any())
            {
                foreach (var step in workflow.Steps)
                {
                    step.WorkflowId = workflow.Id;
                }
            }

            await _context.Workflows.AddAsync(workflow);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns all workflows including their steps.
        /// </summary>
        public async Task<IEnumerable<Workflow>> GetAllAsync()
        {
            return await _context.Workflows
                .Include(w => w.Steps)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Returns a workflow by ID (with all steps).
        /// </summary>
        public async Task<Workflow?> GetByIdAsync(Guid id)
        {
            return await _context.Workflows
                .Include(w => w.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// Updates an existing workflow (and its steps).
        /// </summary>
        public async Task UpdateAsync(Workflow workflow)
        {
            var existingWorkflow = await _context.Workflows
                .Include(w => w.Steps)
                .FirstOrDefaultAsync(w => w.Id == workflow.Id);

            if (existingWorkflow == null)
                throw new Exception($"Workflow with ID {workflow.Id} not found.");

            // Update basic info
            existingWorkflow.Name = workflow.Name;
            existingWorkflow.Description = workflow.Description;

            // Remove steps that no longer exist
            var removedSteps = existingWorkflow.Steps
                .Where(es => !workflow.Steps.Any(ns => ns.Id == es.Id))
                .ToList();

            _context.WorkflowSteps.RemoveRange(removedSteps);

            // Update or add steps
            foreach (var newStep in workflow.Steps)
            {
                var existingStep = existingWorkflow.Steps.FirstOrDefault(s => s.Id == newStep.Id);
                if (existingStep != null)
                {
                    existingStep.StepName = newStep.StepName;
                    existingStep.AssignedTo = newStep.AssignedTo;
                    existingStep.ActionType = newStep.ActionType;
                   // existingStep.NextStep = newStep.NextStep;
                }
                else
                {
                    newStep.WorkflowId = existingWorkflow.Id;
                    _context.WorkflowSteps.Add(newStep);
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a workflow and all its steps.
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var workflow = await _context.Workflows
                .Include(w => w.Steps)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workflow == null)
                throw new Exception($"Workflow with ID {id} not found.");

            _context.WorkflowSteps.RemoveRange(workflow.Steps);
            _context.Workflows.Remove(workflow);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Persists any pending changes (used by Unit of Work or services).
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
