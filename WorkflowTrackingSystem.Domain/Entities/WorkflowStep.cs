using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowTrackingSystem.Domain.Entities
{
    public class WorkflowStep
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public bool RequiresValidation { get; set; }

        // Self-referencing FK (nullable - last step won't have a next step)
        public Guid? NextStepId { get; set; }

        // Navigation Properties - NO [ForeignKey] attributes
        public Workflow Workflow { get; set; } = null!;
        public WorkflowStep? NextStepNavigation { get; set; }
    }
}
