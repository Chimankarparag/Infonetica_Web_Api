public interface IWorkflowService
{
    Task<(bool Success, string? DefinitionId, List<string> Errors)> CreateWorkflowDefinitionAsync(CreateWorkflowDefinitionRequest request);
    Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id);
    Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync();
    
    Task<(bool Success, string? InstanceId, List<string> Errors)> StartWorkflowInstanceAsync(string definitionId);
    Task<WorkflowInstanceResponse?> GetWorkflowInstanceAsync(string instanceId);
    Task<List<WorkflowInstanceResponse>> GetAllWorkflowInstancesAsync();
    
    Task<(bool Success, List<string> Errors)> ExecuteActionAsync(string instanceId, string actionId);
}