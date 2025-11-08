using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Domain.Entities;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.Application.Interfaces.Repositories
{
    public interface IProcessRepository
    {
        Task<IEnumerable<Process>> GetAllAsync(ProcessStatus? status = null);
        Task<Process?> GetByIdAsync(Guid id);
        Task AddAsync(Process process);
        Task UpdateAsync(Process process);
    }
}
