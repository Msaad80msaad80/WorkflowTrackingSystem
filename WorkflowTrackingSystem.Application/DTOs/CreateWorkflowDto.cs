using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Application.DTOs
{
    public class CreateWorkflowDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WorkflowStepDto> Steps { get; set; }
    }


    
}
