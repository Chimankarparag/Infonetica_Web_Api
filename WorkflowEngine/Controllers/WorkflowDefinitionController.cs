using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WorkflowDefinitionController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowDefinitionController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkflowDefinition([FromBody] CreateWorkflowDefinitionRequest request)
    {
        var (success, definitionId, errors) = await _workflowService.CreateWorkflowDefinitionAsync(request);
        
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return CreatedAtAction(nameof(GetWorkflowDefinition), new { id = definitionId }, new { id = definitionId });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkflowDefinition(string id)
    {
        var definition = await _workflowService.GetWorkflowDefinitionAsync(id);
        if (definition == null)
        {
            return NotFound();
        }

        return Ok(definition);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkflowDefinitions()
    {
        var definitions = await _workflowService.GetAllWorkflowDefinitionsAsync();
        return Ok(definitions);
    }
}