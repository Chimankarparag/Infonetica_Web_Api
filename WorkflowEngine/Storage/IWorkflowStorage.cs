public interface IWorkflowStorage
{

    //Task define the contract for how workflow definitions and workflow instances are retrieved, saved, and updated within the storage layer of the Configurable Workflow Engine.

    //defines the contract for how storage operations should be performed
    Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id);
    Task<string> SaveWorkflowDefinitionAsync(WorkflowDefinition definition);
    Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync();
    
    Task<WorkflowInstance?> GetWorkflowInstanceAsync(string id);
    Task<string> SaveWorkflowInstanceAsync(WorkflowInstance instance);
    Task<List<WorkflowInstance>> GetAllWorkflowInstancesAsync();
    Task UpdateWorkflowInstanceAsync(WorkflowInstance instance);
}