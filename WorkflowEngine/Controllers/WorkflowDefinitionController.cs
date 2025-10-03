using Microsoft.AspNetCore.Mvc;
using WorkflowEngine.DTOs;
using WorkflowEngine.Services;

namespace WorkflowEngine.Controllers;

/// <summary>
/// Controller for managing workflow definitions
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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
    /// <param name="request">The workflow definition creation request containing name, states, and actions</param>
    /// <returns>The created workflow definition ID</returns>
    /// <response code="201">Workflow definition created successfully</response>
    /// <response code="400">Invalid request or validation errors</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
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
    /// <param name="id">The unique identifier of the workflow definition</param>
    /// <returns>The workflow definition details</returns>
    /// <response code="200">Workflow definition found and returned</response>
    /// <response code="404">Workflow definition not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <returns>A collection of all workflow definitions in the system</returns>
    /// <response code="200">List of workflow definitions returned successfully</response>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWorkflowDefinitions()
    {
        var definitions = await _workflowService.GetAllWorkflowDefinitionsAsync();
        return Ok(definitions);
    }
}