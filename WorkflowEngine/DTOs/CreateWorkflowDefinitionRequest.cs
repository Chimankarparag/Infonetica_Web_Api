// (DTO) is an object used to encapsulate data and transfer it between different layers or components of an application.


public class CreateWorkflowDefinitionRequest
{
    public string Name { get; set; } = string.Empty;
    public List<State> States { get; set; } = new();
    public List<Action> Actions { get; set; } = new();
}