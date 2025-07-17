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
cd WorkflowEngine
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

## Example and Test API

## 1. Create a Workflow Definition

**Endpoint:**
`POST /api/WorkflowDefinition`

**Request Body:**

```json
{
  "name": "Document Approval",
  "states": [
    {
      "id": "draft",
      "name": "Draft",
      "isInitial": true,
      "isFinal": false,
      "enabled": true,
      "description": "Initial draft stage"
    },
    {
      "id": "review",
      "name": "Review",
      "isInitial": false,
      "isFinal": false,
      "enabled": true,
      "description": "Under review"
    },
    {
      "id": "approved",
      "name": "Approved",
      "isInitial": false,
      "isFinal": true,
      "enabled": true,
      "description": "Final approved state"
    }
  ],
  "actions": [
    {
      "id": "submit",
      "name": "Submit for Review",
      "enabled": true,
      "fromStates": ["draft"],
      "toState": "review",
      "description": "Submit document for review"
    },
    {
      "id": "approve",
      "name": "Approve Document",
      "enabled": true,
      "fromStates": ["review"],
      "toState": "approved",
      "description": "Approve the document"
    }
  ]
}
```

**Expected Response:**

Status: 201 Created

Response contains a valid definitionId

## 2. Start a Workflow Instance

**Endpoint:**
`POST /api/WorkflowInstance/start/{definitionId}`

Use the definitionId returned from step 1.

**Expected Response:**

Status: 201 Created

Response contains an instanceId

## 3. Get Workflow Instance by ID

**Endpoint:**
`GET /api/WorkflowInstance/{id}`

Use the instanceId from step 2.

**Expected Response:**

Shows workflowDefinitionName, currentStateId as draft

History is an empty list

## 4. Execute Action: "submit"

**Endpoint:**
`POST /api/WorkflowInstance/{id}/execute`

**Request Body:**

```json
{
  "actionId": "submit"
}
```

**Expected Response:**

Status: 200 OK

Message: "Action executed successfully"

Instance state changes from draft to review

## 5. Execute Action: "approve"

**Endpoint:**
`POST /api/WorkflowInstance/{id}/execute`

**Request Body:**

```json
{
  "actionId": "approve"
}
```

**Expected Response:**

Status: 200 OK

Message: "Action executed successfully"

Instance state changes from review to approved

## 6. Invalid Action Test (After Final State)

**Endpoint:**
`POST /api/WorkflowInstance/{id}/execute`

**Request Body:**

```json
{
  "actionId": "submit"
}
```

**Expected Response:**

Status: 400 Bad Request

Message: "Action cannot be executed from current state: approved"

## 7. Get All Workflow Instances

**Endpoint:**
`GET /api/WorkflowInstance`

**Expected Response:**

List of all instances

Each entry includes id, currentStateId, createdAt, updatedAt

## 8. Get All Workflow Definitions

**Endpoint:**
`GET /api/WorkflowDefinition`

**Expected Response:**

List of all defined workflows

## 9. Get Workflow Definition by ID

**Endpoint:**
`GET /api/WorkflowDefinition/{id}`

Use the definitionId from step 1.

**Expected Response:**

Complete workflow definition with states and actions
