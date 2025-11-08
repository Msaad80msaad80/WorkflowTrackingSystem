using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowTrackingSystem.Domain.Enums
{
    public enum ProcessStatus
    {
        Pending,
        Active,
        Completed,
        Rejected,
        Running
    }
    public enum StepStatus
    {
        Pending,
        InProgress,
        Completed,
        Rejected
    }
    public enum ActionType
    {
        Approve,
        Reject,
        Input,
        Skip
    }
}
