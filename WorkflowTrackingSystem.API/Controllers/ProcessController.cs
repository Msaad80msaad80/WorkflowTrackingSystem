using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowTrackingSystem.Application.DTOs;
using WorkflowTrackingSystem.Application.Interfaces;
using WorkflowTrackingSystem.Domain.Enums;

namespace WorkflowTrackingSystem.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        /// <summary>
        /// Starts a new process for the given workflow.
        /// </summary>
        /// <param name="request">The workflow ID to start a process from.</param>
        [HttpPost("start")]
        public async Task<IActionResult> StartProcess([FromBody] StartProcessDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var process = await _processService.StartProcessAsync(request.WorkflowId);

            return Ok(process);
        }

        /// <summary>
        /// Executes a step in an active process (simulate action/approval/rejection).
        /// </summary>
        /// <param name="request">The process ID and step/action details.</param>
        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteStep([FromBody] ExecuteStepDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _processService.ExecuteStepAsync(
                request.ProcessId,
                request.StepId,
                request.ActionType
            );

            return Ok(result);
        }

        /// <summary>
        /// Gets the current status and step information for a process.
        /// </summary>
        /// <param name="processId">The unique ID of the process.</param>
        [HttpGet("{processId}")]
        public async Task<IActionResult> GetProcess(Guid processId)
        {
            var process = await _processService.GetProcessByIdAsync(processId);

            if (process == null)
                return NotFound($"Process with ID {processId} not found.");

            return Ok(process);
        }

        /// <summary>
        /// Gets all processes filtered by status.
        /// </summary>
        /// <param name="status">Optional filter: Pending, Active, Completed, Rejected</param>
        [HttpGet]
        public async Task<IActionResult> GetProcesses([FromQuery] ProcessStatus? status = null)
        {
            var processes = await _processService.GetProcessesAsync(status);
            return Ok(processes);
        }
    }
}
