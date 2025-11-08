using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Domain.Entities
{
    public class Process
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Initiator { get; set; } = string.Empty;
        public ProcessStatus Status { get; set; } = ProcessStatus.Pending;
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        // Navigation Properties - NO [ForeignKey] attributes
        public Workflow Workflow { get; set; } = null!;
        public ICollection<ProcessStep> Steps { get; set; } = new List<ProcessStep>();
    }
}
