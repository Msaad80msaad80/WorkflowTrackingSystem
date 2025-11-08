using System;
using System.Collections.Generic;

namespace WorkflowTrackingSystem.Domain.Entities
{
    public class Workflow
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
    }
}
