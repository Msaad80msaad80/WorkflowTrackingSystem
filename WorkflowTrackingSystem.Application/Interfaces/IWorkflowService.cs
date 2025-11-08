using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.DTOs;

namespace WorkflowTrackingSystem.Application.Interfaces
{
    public interface IWorkflowService
    {
        Task<WorkflowDto> CreateWorkflowAsync(CreateWorkflowDto request);
        Task<IEnumerable<WorkflowDto>> GetAllAsync();
        Task<WorkflowDto?> GetByIdAsync(Guid id);
        Task<WorkflowDto?> UpdateAsync(Guid id, UpdateWorkflowDto request);
        Task<bool> DeleteAsync(Guid id);
    }
}
