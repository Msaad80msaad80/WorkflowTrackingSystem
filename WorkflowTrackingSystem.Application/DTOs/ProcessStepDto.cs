using System;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class ProcessStepDto
    {
        public Guid Id { get; set; }                  
        public Guid WorkflowStepId { get; set; }      
        public string StepName { get; set; }  
        public int Order { get; set; }                
        public string Status { get; set; } 
        public Guid StepId { get; set; }



        public string? AssignedTo { get; set; }

        public string? PerformedBy { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? LastAction { get; set; }
    }
}
