using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Domain.Entities
{
    public class ProcessStep
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public Guid WorkflowStepId { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public StepStatus Status { get; set; } = StepStatus.Pending;
        public string? PerformedBy { get; set; }
        public string? Action { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        // Navigation Properties - NO [ForeignKey] attributes
        public Process Process { get; set; } = null!;
        public WorkflowStep WorkflowStep { get; set; } = null!;
    }
}
