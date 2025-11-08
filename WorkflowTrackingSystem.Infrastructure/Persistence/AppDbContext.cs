using Microsoft.EntityFrameworkCore;
using WorkflowTrackingSystem.Domain.Entities;

namespace WorkflowTrackingSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessStep> ProcessSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Workflow
            modelBuilder.Entity<Workflow>()
                .HasMany(w => w.Steps)
                .WithOne(ws => ws.Workflow)
                .HasForeignKey(ws => ws.WorkflowId)
                .OnDelete(DeleteBehavior.Restrict);

            // WorkflowStep self-reference
            modelBuilder.Entity<WorkflowStep>()
                .HasOne(ws => ws.NextStepNavigation)
                .WithMany()
                .HasForeignKey(ws => ws.NextStepId)
                .OnDelete(DeleteBehavior.Restrict);

            // Process -> Workflow (NO inverse navigation)
            modelBuilder.Entity<Process>()
                .HasOne(p => p.Workflow)
                .WithMany()
                .HasForeignKey(p => p.WorkflowId)
                .OnDelete(DeleteBehavior.Restrict);

            // Process -> ProcessSteps
            modelBuilder.Entity<Process>()
                .HasMany(p => p.Steps)
                .WithOne(ps => ps.Process)
                .HasForeignKey(ps => ps.ProcessId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProcessStep -> WorkflowStep (NO inverse navigation)
            modelBuilder.Entity<ProcessStep>()
                .HasOne(ps => ps.WorkflowStep)
                .WithMany()
                .HasForeignKey(ps => ps.WorkflowStepId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}