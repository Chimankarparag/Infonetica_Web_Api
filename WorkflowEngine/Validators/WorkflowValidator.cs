using WorkflowEngine.Models;

namespace WorkflowEngine.Validators;

/// <summary>
/// Validator for workflow definitions
/// </summary>
public class WorkflowValidator
{
    public static ValidationResult ValidateWorkflowDefinition(WorkflowDefinition definition)
    {
        var errors = new List<string>();

        // Check for duplicate state IDs
        var stateIds = definition.States.Select(s => s.Id).ToList();
        if (stateIds.Count != stateIds.Distinct().Count())
        {
            errors.Add("Duplicate state IDs found");
        }

        // Check for duplicate action IDs
        var actionIds = definition.Actions.Select(a => a.Id).ToList();
        if (actionIds.Count != actionIds.Distinct().Count())
        {
            errors.Add("Duplicate action IDs found");
        }

        // Check for exactly one initial state
        var initialStates = definition.States.Where(s => s.IsInitial).ToList();
        if (initialStates.Count != 1)
        {
            errors.Add("Must have exactly one initial state");
        }

        // Validate action references
        foreach (var action in definition.Actions)
        {
            if (!stateIds.Contains(action.ToState))
            {
                errors.Add($"Action {action.Id} references unknown target state: {action.ToState}");
            }

            foreach (var fromState in action.FromStates)
            {
                if (!stateIds.Contains(fromState))
                {
                    errors.Add($"Action {action.Id} references unknown source state: {fromState}");
                }
            }
        }

        return new ValidationResult(errors.Count == 0, errors);
    }
}

/// <summary>
/// Result of validation operations
/// </summary>
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