using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.Interfaces.Repositories;
using WorkflowTrackingSystem.Domain.Entities;
using WorkflowTrackingSystem.Domain.Enums;


namespace WorkflowTrackingSystem.Infrastructure.Persistence.Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly AppDbContext _context;

        public ProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Process>> GetAllAsync(ProcessStatus? status = null)
        {
            IQueryable<Process> query = _context.Processes
                .Include(p => p.Steps)
                .Include(p => p.Workflow);

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            return await query.ToListAsync();
        }

        public async Task<Process?> GetByIdAsync(Guid id)
        {
            return await _context.Processes
                .Include(p => p.Steps)
                .Include(p => p.Workflow)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Process process)
        {
            _context.Processes.Add(process);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Process process)
        {
            _context.Processes.Update(process);
            await _context.SaveChangesAsync();
        }
    }
}
