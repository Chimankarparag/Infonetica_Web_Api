public class InMemoryWorkflowStorage : IWorkflowStorage
{
    private readonly Dictionary<string, WorkflowDefinition> _definitions = new();
    private readonly Dictionary<string, WorkflowInstance> _instances = new();

    public Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id)
    {
        _definitions.TryGetValue(id, out var definition);
        return Task.FromResult(definition);
    }

    public Task<string> SaveWorkflowDefinitionAsync(WorkflowDefinition definition)
    {
        definition.Id = Guid.NewGuid().ToString();
        _definitions[definition.Id] = definition;
        return Task.FromResult(definition.Id);
    }
    public Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync()
    {
        return Task.FromResult(_definitions.Values.ToList());
    }

    public Task<WorkflowInstance?> GetWorkflowInstanceAsync(string id)
    {
        _instances.TryGetValue(id, out var instance);
        return Task.FromResult(instance);
    }

    public Task<string> SaveWorkflowInstanceAsync(WorkflowInstance instance)
    {
        instance.Id = Guid.NewGuid().ToString();
        _instances[instance.Id] = instance;
        return Task.FromResult(instance.Id);
    }

    public Task<List<WorkflowInstance>> GetAllWorkflowInstancesAsync()
    {
        return Task.FromResult(_instances.Values.ToList());
    }

    public Task UpdateWorkflowInstanceAsync(WorkflowInstance instance)
    {
        if (_instances.ContainsKey(instance.Id))
        {
            _instances[instance.Id] = instance;
        }
        return Task.CompletedTask;
    }
}