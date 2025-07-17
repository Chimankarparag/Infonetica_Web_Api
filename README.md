# Workflow Engine API

## Overview
A minimal backend service implementing a configurable state machine-based workflow engine using .NET Core Web API. This service enables defining, managing, and executing workflow instances with full state transition validation and history tracking.

## Objectives
Design and implement a minimal backend service that lets a client:
1. Define one or more workflows as configurable state machines (states + actions).
2. Start workflow instances from a chosen definition.
3. Execute actions to move an instance between states, with full validation.
4. Inspect / list states, actions, definitions, and running instances.

## Technical Stack
- .NET 6.0
- ASP.NET Core Web API
- Swagger/OpenAPI
- In-memory storage implementation

## Project Structure
```
WorkflowEngine/
├── Controllers/
│   ├── WorkflowDefinitionController.cs
│   └── WorkflowInstanceController.cs
├── Models/
│   ├── WorkflowDefinition.cs
│   ├── WorkflowInstance.cs
│   ├── State.cs
│   └── Action.cs
├── Services/
│   ├── IWorkflowService.cs
│   └── WorkflowService.cs
└── Storage/
    ├── IWorkflowStorage.cs
    └── InMemoryWorkflowStorage.cs
```

## API Endpoints

### Workflow Definitions
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/WorkflowDefinition` | List all definitions |
| GET | `/api/WorkflowDefinition/{id}` | Get specific definition |
| POST | `/api/WorkflowDefinition` | Create new definition |

### Workflow Instances
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/WorkflowInstance` | List all instances |
| GET | `/api/WorkflowInstance/{id}` | Get specific instance |
| POST | `/api/WorkflowInstance/start/{definitionId}` | Start new instance |
| POST | `/api/WorkflowInstance/{id}/execute` | Execute action |

## Sample Usage

### 1. Create Workflow Definition
```json
{
    "name": "Document Approval",
    "states": [
        {
            "id": "draft",
            "name": "Draft",
            "isInitial": true
        },
        {
            "id": "review",
            "name": "In Review"
        },
        {
            "id": "approved",
            "name": "Approved",
            "isFinal": true
        }
    ],
    "actions": [
        {
            "id": "submit",
            "name": "Submit",
            "fromStates": ["draft"],
            "toState": "review"
        },
        {
            "id": "approve",
            "name": "Approve",
            "fromStates": ["review"],
            "toState": "approved"
        }
    ]
}
```

### 2. Start Workflow Instance
```bash
curl -X POST http://localhost:5217/api/WorkflowInstance/start/{definitionId}
```

### 3. Execute Action
```bash
curl -X POST http://localhost:5217/api/WorkflowInstance/{instanceId}/execute \
-H "Content-Type: application/json" \
-d '{"actionId": "submit"}'
```

## Getting Started


### Setup
```bash
# Clone the repository
git clone <repository-url>

# Navigate to project directory
cd WorkflowEngine

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

### Development
The service will be available at:
- API: http://localhost:5217
- Swagger Documentation: http://localhost:5217/swagger

## Validation
The service includes comprehensive validation:
- Workflow definition validation (states, actions, transitions)
- State transition validation
- Action execution validation
- Instance existence validation

## History Tracking
Each workflow instance maintains a complete history of transitions including:
- Action executed
- Source and target states
- Timestamp of execution

## Example Workflow Definition
```json
{
    "name": "Document Approval",
    "states": [
        {
            "id": "draft",
            "name": "Draft",
            "isInitial": true,
            "isFinal": false
        },
        {
            "id": "review",
            "name": "In Review",
            "isInitial": false,
            "isFinal": false
        },
        {
            "id": "approved",
            "name": "Approved",
            "isInitial": false,
            "isFinal": true
        }
    ],
    "actions": [
        {
            "id": "submit",
            "name": "Submit for Review",
            "fromStates": ["draft"],
            "toState": "review"
        },
        {
            "id": "approve",
            "name": "Approve Document",
            "fromStates": ["review"],
            "toState": "approved"
        }
    ]
}
```
