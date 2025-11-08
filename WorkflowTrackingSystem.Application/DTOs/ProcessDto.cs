using System;
using System.Collections.Generic;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class ProcessDto
    {
        public Guid Id { get; set; }

        public Guid WorkflowId { get; set; }

        public string WorkflowName { get; set; } = string.Empty;

        public string Initiator { get; set; } = string.Empty;

        public ProcessStatus Status { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public List<ProcessStepDto> Steps { get; set; } = new();
    }
}
