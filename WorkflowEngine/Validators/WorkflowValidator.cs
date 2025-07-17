public class WorkflowValidator
{
    public static ValidationResult ValidateWorkflowDefinition(WorkflowDefinition definition)
    {
        var errors = new List<string>();

        // checking duplicate state Ids
        var stateIds = definition.States.Select(s => s.Id).ToList();
        if (stateIds.Count != stateIds.Distinct().Count())
            errors.Add("Duplicate state IDs found");

        // checking duplicate action IIds
        var actionIds = definition.Actions.Select(a => a.Id).ToList();
        if (actionIds.Count != actionIds.Distinct().Count())
            errors.Add("Duplicate action IDs found");

        // checking exactly one initial state
        var initialStates = definition.States.Where(s => s.IsInitial).ToList();
        if (initialStates.Count != 1)
            errors.Add("Must have exactly one initial state");

        // checking action references
        foreach (var action in definition.Actions)
        {
            if (!stateIds.Contains(action.ToState))
                errors.Add($"Action {action.Id} references unknown target state: {action.ToState}");

            foreach (var fromState in action.FromStates)
            {
                if (!stateIds.Contains(fromState))
                    errors.Add($"Action {action.Id} references unknown source state: {fromState}");
            }
        }

        return new ValidationResult(errors.Count == 0, errors);
    }
}

public class ValidationResult
{
    public bool IsValid { get; }
    public List<string> Errors { get; }

    public ValidationResult(bool isValid, List<string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }
}