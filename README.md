Workflow Tracking System

A .NET 8 Web API project designed for managing Workflows, Processes, and Steps with validation middleware, tracking, and external API simulation.  
This project follows Clean Architecture principles and uses Entity Framework Core for persistence.

---

Project Structure

```

WorkflowTrackingSystem/
│
├── WorkflowTrackingSystem.API           → Web API layer (presentation)
├── WorkflowTrackingSystem.Application   → Application layer (business logic, services, DTOs)
├── WorkflowTrackingSystem.Domain        → Domain entities and enums
├── WorkflowTrackingSystem.Infrastructure→ Database and EF Core configurations

````

---

Tech Stack

- .NET 8 / .NET 7 (Web API)
- Entity Framework Core
- SQL Server
- Clean Architecture
- Dependency Injection
- Validation Middleware
- Swagger (OpenAPI) for testing endpoints

---

Getting Started

rerequisites

Make sure the following tools are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server or LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

---

lone the Repository

```bash
git clone https://github.com/Msaad80msaad80/WorkflowTrackingSystem.git
cd WorkflowTrackingSystem
````

---

Set Up the Database

Make sure your connection string is configured in:

WorkflowTrackingSystem.API/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Your Connection String"
}
```

---

Apply EF Core Migrations

Run the following commands from the Infrastructure project folder:

```bash
cd WorkflowTrackingSystem.Infrastructure
dotnet ef database update --startup-project ../WorkflowTrackingSystem.API
```

This will:

* Create the database automatically
* Apply the initial migration (`InitialCreate`)

---

#Run the Project

Go back to the root or API project and start the API:

```bash
cd ../WorkflowTrackingSystem.API
dotnet run
```

The API will start (usually at):
`https://localhost:5001`
`http://localhost:5000`

---

API Endpoints

1. Workflows Management

Create a Workflow

```
POST /v1/workflows
```

Example Request:

```json
{
  "name": "Approval Process",
  "description": "A workflow to approve purchase requests",
  "steps": [
    { "step_name": "Submit Request", "assigned_to": "employee", "action_type": "input", "next_step": "Manager Approval" },
    { "step_name": "Manager Approval", "assigned_to": "manager", "action_type": "approve_reject", "next_step": "Finance Approval" },
    { "step_name": "Finance Approval", "assigned_to": "finance", "action_type": "approve_reject", "next_step": "Completed" }
  ]
}
```

Retrieve Workflows

```
GET /v1/workflows
```

---

2. Process Execution & Tracking

Start a New Process

```
POST /v1/processes/start
```

Request:

```json
{
  "workflow_id": "GUID_OF_WORKFLOW",
  "initiator": "user123"
}
```

Execute a Step

```
POST /v1/processes/execute
```

Request:

```json
{
  "process_id": "GUID_OF_PROCESS",
  "step_name": "Manager Approval",
  "performed_by": "manager",
  "action": "approve"
}
```

Retrieve Active/Completed Processes

```
GET /v1/processes?status=Active
```

Optional Query Parameters:

* `workflow_id`
* `status` = Active / Completed / Pending
* `assigned_to`

---

3. Validation Middleware

Some workflow steps require validation before proceeding (e.g., Finance Approval).

* Middleware checks if `RequiresValidation = true`
* Simulates an external API call (or can call a real one)
* If validation fails → process stops, and an error message is returned
* Validation results are logged for audit purposes

---

Project Highlights

- Follows Clean Architecture principles
- Uses Dependency Injection for loose coupling
- EF Core for ORM and migrations
- Includes Middleware for external validation simulation
- Built-in Swagger UI for easy testing
- Modular and scalable structure

---

Example Project Flow

Create a new Workflow → `/v1/workflows`
Start a Process from that Workflow → `/v1/processes/start`
Execute each Step → `/v1/processes/execute`
View Active or Completed Processes → `/v1/processes`

---

Developer Setup Commands

```bash
# Clone repository
git clone https://github.com/your repo name/WorkflowTrackingSystem.git

# Navigate to API folder
cd WorkflowTrackingSystem/WorkflowTrackingSystem.API

# Restore dependencies
dotnet restore

# Run migrations
cd ../WorkflowTrackingSystem.Infrastructure
dotnet ef database update --startup-project ../WorkflowTrackingSystem.API

# Run the API
cd ../WorkflowTrackingSystem.API
dotnet run
```

Open your browser at:
`https://localhost:5001/swagger`

---

Author

Mohamed Saad
Senior .NET Developer
msaad80gmail.com
[GitHub Profile](https://github.com/Msaad80msaad80)

---

License

This project is open-source and available under the [MIT License](LICENSE).

```

