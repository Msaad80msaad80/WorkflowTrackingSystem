using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Domain.Models
{
    public class ProcessStep
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public string StepName { get; set; }
        public string PerformedBy { get; set; }
        public string Action { get; set; }
        public DateTime ExecutedAt { get; set; }
        public bool IsValidated { get; set; }
        public string ValidationMessage { get; set; }
    }
}
