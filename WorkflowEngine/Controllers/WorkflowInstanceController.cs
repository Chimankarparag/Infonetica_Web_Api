using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.DTOs;
using WorkflowEngine.Services;

namespace WorkflowEngine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowInstanceController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowInstanceController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    /// <summary>
    /// Starts a new workflow instance
    /// </summary>
    /// <param name="definitionId">The workflow definition ID</param>
    /// <returns>The created workflow instance ID</returns>
    [HttpPost("start/{definitionId}")]
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
    /// Gets a workflow instance by ID
    /// </summary>
    /// <param name="id">The workflow instance ID</param>
    /// <returns>The workflow instance</returns>
    [HttpGet("{id}")]
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
    /// Executes an action on a workflow instance
    /// </summary>
    /// <param name="id">The workflow instance ID</param>
    /// <param name="request">The action execution request</param>
    /// <returns>Success status of the action execution</returns>
    [HttpPost("{id}/execute")]
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
    /// Gets all workflow instances
    /// </summary>
    /// <returns>A list of all workflow instances</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllWorkflowInstances()
    {
        var instances = await _workflowService.GetAllWorkflowInstancesAsync();
        return Ok(instances);
    }
}