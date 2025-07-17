using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkflowInstanceController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowInstanceController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }
    // similarly creating the endpoints for the workflow instance operations
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

    [HttpPost("{id}/execute")]
    public async Task<IActionResult> ExecuteAction(string id, [FromBody] ExecuteActionRequest request)
    {
        //the state change is handled by the service layer - actions - in ExecuteActionAsync method
        var (success, errors) = await _workflowService.ExecuteActionAsync(id, request.ActionId);
        
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return Ok(new { message = "Action executed successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkflowInstances()
    {
        var instances = await _workflowService.GetAllWorkflowInstancesAsync();
        return Ok(instances);
    }
}