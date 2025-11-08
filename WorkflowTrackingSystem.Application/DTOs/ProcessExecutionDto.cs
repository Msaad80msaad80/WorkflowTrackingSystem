using System;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class ProcessExecutionDto
    {
        public Guid ProcessId { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;

    }
}
