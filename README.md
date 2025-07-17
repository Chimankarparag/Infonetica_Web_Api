                                    # Workflow Engine API

## Overview
A configurable state machine-based workflow engine built with .NET Core Web API. Enables defining, managing, and executing workflow instances with state transition validation and history tracking.

## Key Features
- Define custom workflows with states and actions
- Start and manage workflow instances
- Execute state transitions with validation
- Track complete workflow history
- RESTful API with Swagger documentation

## Quick Start

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Running the Application
```bash
# Clone and run
git clone https://github.com/Chimankarparag/Infonetica_Task.git
cd workflow-engine
dotnet restore
dotnet build
dotnet run
```

Access the application:
- API: http://localhost:5217
- Swagger UI: http://localhost:5217/swagger

## API Endpoints

### Workflow Definitions
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/WorkflowDefinition` | List all definitions |
| GET | `/api/WorkflowDefinition/{id}` | Get definition by ID |
| POST | `/api/WorkflowDefinition` | Create definition |

### Workflow Instances
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/WorkflowInstance` | List all instances |
| GET | `/api/WorkflowInstance/{id}` | Get instance by ID |
| POST | `/api/WorkflowInstance/start/{definitionId}` | Start instance |
| POST | `/api/WorkflowInstance/{id}/execute` | Execute action |

## Example Usage

### Create a Workflow Definition
```bash
curl -X POST http://localhost:5217/api/WorkflowDefinition \
-H "Content-Type: application/json" \
-d '{
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
}'
```

### Execute Workflow Actions
```bash
# Start instance
curl -X POST http://localhost:5217/api/WorkflowInstance/start/{definitionId}

# Execute action
curl -X POST http://localhost:5217/api/WorkflowInstance/{instanceId}/execute \
-H "Content-Type: application/json" \
-d '{"actionId": "submit"}'
```

## Project Structure
```
WorkflowEngine/
├── Controllers/
├── Models/
├── Services/
└── Storage/
```

## Technical Details

### Implementation
- .NET 8.0 and ASP.NET Core Web API
- In-memory storage
- RESTful API with JSON payloads
- Swagger/OpenAPI documentation

### Assumptions
- Single-user system (no auth)
- In-memory data storage
- Unique IDs within workflows
- Linear workflow progression

### Limitations
- No persistent storage
- No concurrent execution handling
- No workflow versioning
- Basic validation only
- No conditional transitions
- No automatic transitions/timeouts

## Validation Features
- Workflow definition validation
- State transition validation
- Action execution validation
- History tracking of all transitions

