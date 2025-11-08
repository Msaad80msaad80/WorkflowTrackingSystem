using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class WorkflowStepDto
    {
        public string StepName { get; set; }
        public string AssignedTo { get; set; }
        public string ActionType { get; set; }
        public string NextStep { get; set; }
        public bool RequiresValidation { get; set; } = false;
        public Guid Id { get; internal set; }
    }
}
