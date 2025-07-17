public class WorkflowService : IWorkflowService
{
    private readonly IWorkflowStorage _storage;

    public WorkflowService(IWorkflowStorage storage)
    {
        _storage = storage;
    }

    public async Task<(bool Success, string? DefinitionId, List<string> Errors)> CreateWorkflowDefinitionAsync(CreateWorkflowDefinitionRequest request)
    {
        var definition = new WorkflowDefinition
        {
            Name = request.Name,
            States = request.States,
            Actions = request.Actions
        };

        var validation = WorkflowValidator.ValidateWorkflowDefinition(definition);
        if (!validation.IsValid)
        {
            return (false, null, validation.Errors);
        }

        var definitionId = await _storage.SaveWorkflowDefinitionAsync(definition);
        return (true, definitionId, new List<string>());
    }

    public async Task<(bool Success, List<string> Errors)> ExecuteActionAsync(string instanceId, string actionId)
    {
        var instance = await _storage.GetWorkflowInstanceAsync(instanceId);
        if (instance == null)
            return (false, new List<string> { "Workflow instance not found" });

        var definition = await _storage.GetWorkflowDefinitionAsync(instance.WorkflowDefinitionId);
        if (definition == null)
            return (false, new List<string> { "Workflow definition not found" });

        var action = definition.Actions.FirstOrDefault(a => a.Id == actionId);
        if (action == null)
            return (false, new List<string> { "Action not found in workflow definition" });

        if (!action.Enabled)
            return (false, new List<string> { "Action is disabled" });

        var currentState = definition.States.FirstOrDefault(s => s.Id == instance.CurrentStateId);
        if (currentState == null)
            return (false, new List<string> { "Current state not found" });

        if (currentState.IsFinal)
            return (false, new List<string> { "Cannot execute actions on final states" });

        if (!action.FromStates.Contains(instance.CurrentStateId))
            return (false, new List<string> { $"Action cannot be executed from current state: {instance.CurrentStateId}" });

        var targetState = definition.States.FirstOrDefault(s => s.Id == action.ToState);
        if (targetState == null)
            return (false, new List<string> { "Target state not found" });

        // Execute the action
        var historyEntry = new HistoryEntry
        {
            ActionId = actionId,
            FromStateId = instance.CurrentStateId,
            ToStateId = action.ToState,
            ExecutedAt = DateTime.UtcNow
        };

        instance.History.Add(historyEntry);
        instance.CurrentStateId = action.ToState;
        instance.UpdatedAt = DateTime.UtcNow;

        await _storage.UpdateWorkflowInstanceAsync(instance);
        return (true, new List<string>());
    }
    public async Task<WorkflowDefinition?> GetWorkflowDefinitionAsync(string id)
    {
        return await _storage.GetWorkflowDefinitionAsync(id);
    }
    public async Task<List<WorkflowDefinition>> GetAllWorkflowDefinitionsAsync()
    {
        return await _storage.GetAllWorkflowDefinitionsAsync();
    }
    public async Task<(bool Success, string? InstanceId, List<string> Errors)> StartWorkflowInstanceAsync(string definitionId)
    {
        var definition = await _storage.GetWorkflowDefinitionAsync(definitionId);
        if (definition == null)
            return (false, null, new List<string> { "Workflow definition not found" });

        var initialState = definition.States.FirstOrDefault(s => s.IsInitial);
        if (initialState == null)
            return (false, null, new List<string> { "No initial state defined in workflow" });

        var instance = new WorkflowInstance
        {
            Id = Guid.NewGuid().ToString(),
            WorkflowDefinitionId = definitionId,
            CurrentStateId = initialState.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var instanceId = await _storage.SaveWorkflowInstanceAsync(instance);
        return (true, instanceId, new List<string>());
    }
    public async Task<WorkflowInstanceResponse?> GetWorkflowInstanceAsync(string instanceId)
    {
        var instance = await _storage.GetWorkflowInstanceAsync(instanceId);
        if (instance == null)
            return null;

        var definition = await _storage.GetWorkflowDefinitionAsync(instance.WorkflowDefinitionId);
        if (definition == null)
            return null;

        return new WorkflowInstanceResponse
        {
            Id = instance.Id,
            WorkflowDefinitionName = definition.Name,
            CurrentStateId = instance.CurrentStateId,
            History = instance.History,
            CreatedAt = instance.CreatedAt,
            UpdatedAt = instance.UpdatedAt
        };
    }
    public async Task<List<WorkflowInstanceResponse>> GetAllWorkflowInstancesAsync()
    {
        var instances = await _storage.GetAllWorkflowInstancesAsync();
        var definitions = await _storage.GetAllWorkflowDefinitionsAsync();

        return instances.Select(instance =>
        {
            var definition = definitions.FirstOrDefault(d => d.Id == instance.WorkflowDefinitionId);
            return new WorkflowInstanceResponse
            {
                Id = instance.Id,
                WorkflowDefinitionName = definition?.Name ?? "Unknown",
                CurrentStateId = instance.CurrentStateId,
                History = instance.History,
                CreatedAt = instance.CreatedAt,
                UpdatedAt = instance.UpdatedAt
            };
        }).ToList();
    }
    
}