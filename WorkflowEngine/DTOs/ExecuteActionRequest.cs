namespace WorkflowEngine.DTOs;

/// <summary>
/// DTO for executing an action on a workflow instance
/// </summary>
public class ExecuteActionRequest
{
    public string ActionId { get; set; } = string.Empty;
}