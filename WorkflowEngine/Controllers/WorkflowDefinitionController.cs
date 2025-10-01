using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.DTOs;
using WorkflowEngine.Services;

namespace WorkflowEngine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowDefinitionController : ControllerBase
{
    private readonly IWorkflowService _workflowService;

    public WorkflowDefinitionController(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
    }

    /// <summary>
    /// Creates a new workflow definition
    /// </summary>
    /// <param name="request">The workflow definition creation request</param>
    /// <returns>The created workflow definition ID</returns>
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

    /// <summary>
    /// Gets a workflow definition by ID
    /// </summary>
    /// <param name="id">The workflow definition ID</param>
    /// <returns>The workflow definition</returns>
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

    /// <summary>
    /// Gets all workflow definitions
    /// </summary>
    /// <returns>A list of all workflow definitions</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllWorkflowDefinitions()
    {
        var definitions = await _workflowService.GetAllWorkflowDefinitionsAsync();
        return Ok(definitions);
    }
}