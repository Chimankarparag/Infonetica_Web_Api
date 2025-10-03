using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.DTOs;
using WorkflowEngine.Services;

namespace WorkflowEngine.Controllers;

/// <summary>
/// Controller for managing workflow instances and their execution
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WorkflowInstanceController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowInstanceController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    /// <summary>
    /// Starts a new workflow instance from a workflow definition
    /// </summary>
    /// <param name="definitionId">The unique identifier of the workflow definition to instantiate</param>
    /// <returns>The created workflow instance ID</returns>
    /// <response code="201">Workflow instance created successfully</response>
    /// <response code="400">Invalid definition ID or workflow definition not found</response>
    [HttpPost("start/{definitionId}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartWorkflowInstance(string definitionId)
    {
        var (success, instanceId, errors) = await _workflowService.StartWorkflowInstanceAsync(definitionId);
        
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return CreatedAtAction(nameof(GetWorkflowInstance), new { id = instanceId }, new { id = instanceId });
    }

    /// <summary>
    /// Gets a workflow instance by ID with its current state and execution history
    /// </summary>
    /// <param name="id">The unique identifier of the workflow instance</param>
    /// <returns>The workflow instance details including current state</returns>
    /// <response code="200">Workflow instance found and returned</response>
    /// <response code="404">Workflow instance not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkflowInstance(string id)
    {
        var instance = await _workflowService.GetWorkflowInstanceAsync(id);
        if (instance == null)
        {
            return NotFound();
        }

        return Ok(instance);
    }

    /// <summary>
    /// Executes an action on a workflow instance, potentially changing its state
    /// </summary>
    /// <param name="id">The unique identifier of the workflow instance</param>
    /// <param name="request">The action execution request containing the action ID to execute</param>
    /// <returns>Success status of the action execution</returns>
    /// <response code="200">Action executed successfully</response>
    /// <response code="400">Invalid request, action not found, or action cannot be executed in current state</response>
    /// <response code="404">Workflow instance not found</response>
    [HttpPost("{id}/execute")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ExecuteAction(string id, [FromBody] ExecuteActionRequest request)
    {
        // The state change is handled by the service layer in ExecuteActionAsync method
        var (success, errors) = await _workflowService.ExecuteActionAsync(id, request.ActionId);
        
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return Ok(new { message = "Action executed successfully" });
    }

    /// <summary>
    /// Gets all workflow instances in the system
    /// </summary>
    /// <returns>A collection of all workflow instances with their current states</returns>
    /// <response code="200">List of workflow instances returned successfully</response>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWorkflowInstances()
    {
        var instances = await _workflowService.GetAllWorkflowInstancesAsync();
        return Ok(instances);
    }
}