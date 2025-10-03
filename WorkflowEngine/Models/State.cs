namespace WorkflowEngine.Models;

/// <summary>
/// Represents a state in a workflow
/// </summary>
public class State
{
    /// <summary>
    /// Unique identifier for the state
    /// </summary>
    /// <example>pending</example>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Display name of the state
    /// </summary>
    /// <example>Pending Review</example>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates if this is the initial state of the workflow
    /// </summary>
    public bool IsInitial { get; set; }
    
    /// <summary>
    /// Indicates if this is a final state of the workflow
    /// </summary>
    public bool IsFinal { get; set; }
    
    /// <summary>
    /// Indicates if the state is currently enabled
    /// </summary>
    public bool Enabled { get; set; } = true;
    
    /// <summary>
    /// Optional description of the state
    /// </summary>
    public string? Description { get; set; }
}