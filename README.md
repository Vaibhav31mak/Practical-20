# Practical20

# Overview

**Practical20** is a modern ASP.NET Core Web API built using **.NET 10**, following:

- SOLID Principles
- Object-Oriented Programming (OOP)
- Clean layered architecture
- Repository + Unit of Work patterns
- Result Pattern for standardized API responses
- Audit logging using EF Core intercepting via overridden `SaveChanges` / `SaveChangesAsync`

The solution demonstrates scalable enterprise-grade backend practices with proper separation of concerns.

---

# Features

## Student CRUD API

- Create Student
- Get All Students
- Get Student By Id
- Update Student
- Delete Student

## Audit Logging

- Automatic audit tracking
- Tracks entity changes
- Logs create/update/delete actions
- Implemented by overriding:
  - `SaveChanges()`
  - `SaveChangesAsync()`

## Result Pattern

- Standardized success/failure responses
- Cleaner service layer handling
- Eliminates exception-driven business flow

## Architecture Features

- Layered architecture
- Repository Pattern
- Unit of Work Pattern
- DTO usage
- AutoMapper integration
- Global exception handling
- Minimal API endpoint organization

---

# Tech Stack

| Technology | Purpose |
|---|---|
| .NET 10 | Backend Framework |
| ASP.NET Core Minimal API | API Development |
| Entity Framework Core | ORM |
| SQL Server | Database |
| AutoMapper | Object Mapping |
| Swagger/OpenAPI | API Documentation |
| Result Pattern | Standardized Responses |
| Repository Pattern | Data Access |
| Unit of Work | Transaction Management |

---

# Architecture & Design Principles

## SOLID Principles

The project follows:

- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

## OOP Principles

- Encapsulation
- Abstraction
- Inheritance
- Polymorphism

---

# Audit Logging

Audit logging is implemented centrally inside EF Core DbContext.

## Internals

`SaveChanges()` and `SaveChangesAsync()` are overridden to:

- Track entity state changes
- Capture modifications automatically
- Store audit records in `AuditLog` table

This ensures auditing is automatic and consistent across the application.

---

# Result Pattern

## What is Result<T>?

The application uses a custom `Result<T>` pattern for returning standardized responses.

Instead of throwing exceptions for business validation failures:

```csharp
return Result<StudentDto>.Failure("Student not found");
```

Success example:

```csharp
return Result<StudentDto>.Success(studentDto);
```

---

## Why Result Pattern?

Benefits:

- Predictable API responses
- Cleaner service logic
- Avoids exception-driven control flow
- Easier validation handling
- Better maintainability

---

## Success Response Example

```json
{
  "isSuccess": true,
  "data": {
    "id": 1,
    "name": "Vaibhav"
  },
  "errors": []
}
```

---

## Failure Response Example

```json
{
  "isSuccess": false,
  "data": null,
  "errors": [
    "Student not found"
  ]
}
```

---

# Setup Instructions

## Prerequisites

Install:

- .NET 10 SDK
- SQL Server / SQL Server LocalDB
- Visual Studio 2022 or VS Code

---

# Configure Database

Update connection string in:

```text
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Practical20Db;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

---

# Apply Migrations

```bash
dotnet ef database update
```

---

# Run Application

```bash
dotnet run
```

Swagger UI:

```text
https://localhost:<port>/swagger
```

---

# API Endpoints

## Student Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/students` | Get all students |
| GET | `/students/{id}` | Get student by id |
| POST | `/students` | Create student |
| PUT | `/students/{id}` | Update student |
| DELETE | `/students/{id}` | Delete student |

---

## Audit Log Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/auditlogs` | Get all audit logs |

---

# Folder Structure

```text
Practical20.Api/
├── Endpoints/
│   ├── BaseEndpoint.cs
│   ├── StudentEndpoints/
│   │   ├── CreateStudentEndpoint.cs
│   │   ├── DeleteStudentEndpoint.cs
│   │   ├── GetAllStudentsEndpoint.cs
│   │   ├── GetStudentByIdEndpoint.cs
│   │   └── UpdateStudentEndpoint.cs
│   ├── AuditLogEndpoints/
│   │   └── GetAllAuditLogsEndpoint.cs
│
├── Exceptions/
│   ├── NotFoundException.cs
│   └── GlobalExceptionHandler.cs
│
├── GlobalUsings.cs
├── Program.cs
├── appsettings.json
├── SwaggerDemo.http
│
├── Properties/
│   └── launchSettings.json

Practical20.Application/
├── Contracts/
│   ├── IStudentService.cs
│   └── IAuditLogService.cs
│
├── DependencyInjection/
│   └── ApplicationServicesRegistration.cs
│
├── Dtos/
│   ├── Students/StudentDtos.cs
│   └── AuditLogs/AuditLogDtos.cs
│
├── Mapping/
│   └── StudentProfile.cs
│
├── Services/
│   ├── StudentService.cs
│   └── AuditLogService.cs
│
└── GlobalUsings.cs

Practical20.Domain/
├── Common/
│   ├── AuditingConracts/
│   │   ├── IConcurrencyCheck.cs
│   │   ├── ICreatable.cs
│   │   ├── IEntity.cs
│   │   ├── ISoftDeletable.cs
│   │   └── IUpdatable.cs
│   │
│   ├── AuditingEntities/
│   │   └── BaseEntity.cs
│   │
│   └── ResultPattern/
│       └── Result.cs
│
├── Entities/
│   ├── Student.cs
│   └── AuditLog.cs
│
└── GlobalUsings.cs

Practical20.Infrastructure/
├── Data/
│   ├── Configurations/
│   │   ├── StudentConfiguration.cs
│   │   └── AuditLogConfiguration.cs
│   │
│   ├── DbContext/
│   │   ├── StudentDbContext.cs
│   │   └── StudentDbContext.Auditing.cs
│   │
│   └── DependencyInjection/
│       └── InfrastructureServiceRegistration.cs
│
├── Migrations/
│   ├── 20260523112949_InitialCreate.cs
│   ├── 20260523112949_InitialCreate.Designer.cs
│   └── StudentDbContextModelSnapshot.cs
│
├── Repositories/
│   ├── Contracts/
│   │   └── IBaseRepository.cs
│   │
│   └── Implementations/
│       └── BaseRepository.cs
│
├── UnitOfWorkPattern/
│   ├── IUnitOfWork.cs
│   └── UnitOfWork.cs
│
└── GlobalUsings.cs
```

---

# Swagger Support

Swagger/OpenAPI is enabled for testing APIs easily.

Example:

- Execute CRUD operations
- Test API contracts
- Inspect request/response schemas

---
