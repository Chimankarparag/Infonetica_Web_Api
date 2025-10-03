namespace WorkflowEngine.Models;

/// <summary>
/// Represents an action that transitions between workflow states
/// </summary>
public class Action
{
    /// <summary>
    /// Unique identifier for the action
    /// </summary>
    /// <example>approve</example>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Display name of the action
    /// </summary>
    /// <example>Approve Order</example>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates if the action is currently enabled
    /// </summary>
    public bool Enabled { get; set; } = true;
    
    /// <summary>
    /// List of state IDs from which this action can be executed
    /// </summary>
    public List<string> FromStates { get; set; } = new();
    
    /// <summary>
    /// The target state ID that this action transitions to
    /// </summary>
    /// <example>approved</example>
    public string ToState { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional description of the action
    /// </summary>
    public string? Description { get; set; }
}