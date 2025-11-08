using System;
using System.Collections.Generic;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.DTOs
{
    /// <summary>
    /// DTO used for updating an existing workflow and its steps.
    /// </summary>
    public class UpdateWorkflowDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Optional: List of updated or newly added workflow steps.
        /// </summary>
        public List<UpdateWorkflowStepDto> Steps { get; set; } = new();
    }

    /// <summary>
    /// Represents a single workflow step to be updated or added.
    /// </summary>
    public class UpdateWorkflowStepDto
    {
        public Guid? Id { get; set; } // Null means it's a new step

        public string StepName { get; set; } = string.Empty;

        public string AssignedTo { get; set; } = string.Empty;

        public ActionType ActionType { get; set; }

        public Guid? NextStep { get; set; }

        public bool RequiresValidation { get; set; }
    }
}
