public class WorkflowInstanceResponse
{
    public string Id { get; set; } = string.Empty;
    public string WorkflowDefinitionId { get; set; } = string.Empty;
    public string WorkflowDefinitionName { get; set; } = string.Empty;
    public string CurrentStateId { get; set; } = string.Empty;
    public List<HistoryEntry> History { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

//  the DTO are needed for services