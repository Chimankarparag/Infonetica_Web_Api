namespace WorkflowEngine.DTOs;

/// <summary>
/// Request model for executing an action on a workflow instance
/// </summary>
public class ExecuteActionRequest
{
    /// <summary>
    /// The unique identifier of the action to execute
    /// </summary>
    /// <example>approve-order</example>
    public string ActionId { get; set; } = string.Empty;
}