public class WorkflowInstance
{
    //declaring model for the instance 
    public string Id { get; set; } = string.Empty;
    public string WorkflowDefinitionId { get; set; } = string.Empty;
    //the current state id
    public string CurrentStateId { get; set; } = string.Empty;
    public List<HistoryEntry> History { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
//making history to monitor the state transition due to action
public class HistoryEntry
{
    public string ActionId { get; set; } = string.Empty;
    public string FromStateId { get; set; } = string.Empty;
    public string ToStateId { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}

//used later in example

// GET /api/WorkflowInstance/{id}

/*
{
    "id": "instance-123",
    "currentStateId": "review",
    "history": [
        {
            "actionId": "submit",
            "fromStateId": "draft",
            "toStateId": "review",
            "executedAt": "2025-07-18T10:30:00Z"
        }
        
    ]
}
*/