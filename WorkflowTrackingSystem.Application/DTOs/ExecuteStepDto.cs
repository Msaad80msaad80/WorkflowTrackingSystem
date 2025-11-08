using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class ExecuteStepDto
    {
        public Guid ProcessId { get; set; }
        public Guid StepId { get; set; }

      
        public ActionType ActionType { get; set; }
        public string StepName { get; set; }
        public string PerformedBy { get; set; }
        public string Action { get; set; }
    }
}
