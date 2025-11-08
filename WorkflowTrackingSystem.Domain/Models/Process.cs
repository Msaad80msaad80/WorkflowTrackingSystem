using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Domain.Models
{
    public class Process
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public Workflow Workflow { get; set; }
        public string Initiator { get; set; }
        public string Status { get; set; } // Active, Completed
        public ICollection<ProcessStep> ExecutedSteps { get; set; } = new List<ProcessStep>();
    }
}
