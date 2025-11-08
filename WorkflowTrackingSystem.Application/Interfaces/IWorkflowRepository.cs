using WorkflowTrackingSystem.Domain.Entities;

namespace WorkflowTrackingSystem.Domain.Interfaces
{
    public interface IWorkflowRepository
    {
        /// <summary>
        /// Adds a new workflow definition (with its steps).
        /// </summary>
        /// <param name="workflow">The workflow entity to add.</param>
        Task AddAsync(Workflow workflow);

        /// <summary>
        /// Retrieves all workflows including their steps.
        /// </summary>
        /// <returns>A list of all workflows.</returns>
        Task<IEnumerable<Workflow>> GetAllAsync();

        /// <summary>
        /// Retrieves a workflow by its unique ID, including steps.
        /// </summary>
        /// <param name="id">The workflow ID.</param>
        /// <returns>The workflow entity or null if not found.</returns>
        Task<Workflow?> GetByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing workflow definition.
        /// </summary>
        /// <param name="workflow">The updated workflow entity.</param>
        Task UpdateAsync(Workflow workflow);

        /// <summary>
        /// Deletes a workflow by its ID.
        /// </summary>
        /// <param name="id">The workflow ID to delete.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Persists any pending changes to the database.
        /// </summary>
        Task SaveChangesAsync();
    }
}
