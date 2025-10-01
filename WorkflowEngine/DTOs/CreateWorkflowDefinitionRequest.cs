using WorkflowEngine.Models;

namespace WorkflowEngine.DTOs;

/// <summary>
/// DTO for creating a workflow definition
/// </summary>
public class CreateWorkflowDefinitionRequest
{
    public string Name { get; set; } = string.Empty;
    public List<State> States { get; set; } = new();
    public List<Action> Actions { get; set; } = new();
}