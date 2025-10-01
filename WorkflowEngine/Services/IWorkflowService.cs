using WorkflowEngine.DTOs;
using WorkflowEngine.Models;

namespace WorkflowEngine.Services;

/// <summary>
/// Interface defining workflow service operations
/// </summary>
public interface IWorkflowService
{
    // Workflow Definition operations
    Task<(bool Success, string? DefinitionId, List<string> Errors)> CreateWorkflowDefinitionAsync(CreateWorkflowDefinitionRequest request);
    Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id);
    Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync();
    
    // Workflow Instance operations
    Task<(bool Success, string? InstanceId, List<string> Errors)> StartWorkflowInstanceAsync(string definitionId);
    Task<WorkflowInstanceResponse?> GetWorkflowInstanceAsync(string instanceId);
    Task<List<WorkflowInstanceResponse>> GetAllWorkflowInstancesAsync();
    
    // Action execution
    Task<(bool Success, List<string> Errors)> ExecuteActionAsync(string instanceId, string actionId);
}