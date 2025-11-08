using Microsoft.AspNetCore.Mvc;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Application.Interfaces;

namespace WorkflowTrackingSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkflowsController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowsController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        /// <summary>
        /// Create a new workflow definition.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkflow([FromBody] CreateWorkflowDto request)
        {
            if (request == null)
                return BadRequest("Invalid workflow request.");

            var result = await _workflowService.CreateWorkflowAsync(request);
            return CreatedAtAction(nameof(GetWorkflowById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Get all workflows.
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetAllWorkflows()
        {
            var result = await _workflowService.GetAllAsync();

            if (result == null || !result.Any())
                return NotFound("No workflows found.");

            return Ok(result);
        }

        /// <summary>
        /// Get workflow details by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWorkflowById(Guid id)
        {
            var result = await _workflowService.GetByIdAsync(id);
            if (result == null)
                return NotFound($"Workflow with ID {id} not found.");

            return Ok(result);
        }

        /// <summary>
        /// Update an existing workflow.
        /// </summary>
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateWorkflow(Guid id, [FromBody] UpdateWorkflowDto request)
        {
            if (request == null)
                return BadRequest("Invalid workflow data.");

            var result = await _workflowService.UpdateAsync(id, request);
            if (result == null)
                return NotFound($"Workflow with ID {id} not found.");

            return Ok(result);
        }

        /// <summary>
        /// Delete a workflow by ID.
        /// </summary>
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteWorkflow(Guid id)
        {
            var success = await _workflowService.DeleteAsync(id);
            if (!success)
                return NotFound($"Workflow with ID {id} not found.");

            return NoContent();
        }
    }
}
