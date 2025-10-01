using WorkflowEngine.Models;

namespace WorkflowEngine.Storage;

/// <summary>
/// Interface defining storage operations for workflow definitions and instances
/// </summary>
public interface IWorkflowStorage
{
    // Workflow Definition operations
    Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id);
    Task<string> SaveWorkflowDefinitionAsync(WorkflowDefinition definition);
    Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync();
    
    // Workflow Instance operations
    Task<WorkflowInstance?> GetWorkflowInstanceAsync(string id);
    Task<string> SaveWorkflowInstanceAsync(WorkflowInstance instance);
    Task<List<WorkflowInstance>> GetAllWorkflowInstancesAsync();
    Task UpdateWorkflowInstanceAsync(WorkflowInstance instance);
}