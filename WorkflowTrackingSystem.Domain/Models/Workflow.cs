using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Domain.Models
{
    public class Workflow
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
    }
}
