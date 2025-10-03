using WorkflowEngine.Models;

namespace WorkflowEngine.DTOs;

/// <summary>
/// Request model for creating a new workflow definition
/// </summary>
public class CreateWorkflowDefinitionRequest
{
    /// <summary>
    /// The name of the workflow definition
    /// </summary>
    /// <example>Order Processing Workflow</example>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// List of states that define the workflow stages
    /// </summary>
    public List<State> States { get; set; } = new();
    
    /// <summary>
    /// List of actions that can be executed to transition between states
    /// </summary>
    public List<Models.Action> Actions { get; set; } = new();
}