public class WorkflowDefinition
{
    //to create the workflows we define them with a unique id (later removing duplicates and ensuring unique)
    //each workflow should have multiple states and actions as required
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<State> States { get; set; } = new();
    public List<Action> Actions { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}