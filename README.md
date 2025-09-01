# Simple-RIS-APIs

A Radiology Information System (RIS) backend API built with ASP.NET Core 9.0, implementing authentication, authorization, and CRUD operations for patients, doctors, and radiological studies.

## Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Authentication & Authorization](#authentication--authorization)
- [Setup Instructions](#setup-instructions)
- [Development Guidelines](#development-guidelines)

## Project Overview

This RIS system provides a RESTful API for managing:

- Patient records and medical studies
- Doctor profiles and expertise
- Radiological services and studies
- User authentication and role-based authorization

## Architecture

The project follows Clean Architecture principles with these layers:

- **APIs**: Controllers and Services (Presentation Layer)
- **Core**: DTOs, Interfaces, and Domain Models (Domain Layer)
- **Data**: DbContext, Repositories, and Data Access (Infrastructure Layer)

## Project Structure

```
RIS/
├── APIs/                   # API Controllers and Services
│   ├── Controllers/            # REST API endpoints
│   ├── Services/               # Business logic
│   └── Configs/                # Configuration classes
├── Core/                   # Core Domain Layer
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Interfaces/             # Contracts for services & repos
|   |   ├── Controllers/            
│   |   └── Services/               
│   └── Models/                 # Domain entities
|   └── IUnitOfWork             # Unit of work interface
└── Data/                   # Data Access Layer
    ├── Repositories/           # Data access implementation
    └── AppDbContext.cs         # EF Core DbContext
    └── UnitOfWork.cs           # Unit of work implementation
```

## Technologies Used

- ASP.NET Core 9.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI
- Castle.Core for Proxies

## Database Schema

The system uses the following main entities:

- **Persons**: Base user information
- **Patients**: Patient-specific data
- **Doctors**: Doctor profiles and expertise
- **Services**: Available radiological services
- **Studies**: Medical examination records

## API Endpoints

### Authentication

- POST `/api/Auth/register` - Register new user
- POST `/api/Auth/login` - User login

### Patients

- GET `/api/Patients` - Get patient details
- POST `/api/Patients` - Register new patient
- PUT `/api/Patients` - Update patient info
- DELETE `/api/Patients/{id}` - Delete patient

### Doctors

- POST `/api/Doctors` - Register new doctor
- PUT `/api/Doctors` - Update doctor info
- GET `/api/Doctors/studies?page=1&limit=10` - Get doctor's studies with pagination

### Studies

- POST `/api/Studies` - Create new study (Patients)
- PUT `/api/Studies` - Update study info (Patients)
- PUT `/api/Studies/complete/{studyId}` - Mark study status as completed (Doctors)
- Delete `/api/Studies/cancel/{studyId}` - Cancel a pending study (Patients)

## Authentication & Authorization

The system uses JWT (JSON Web Tokens) for authentication with role-based authorization:

- **Patient Role**: Access to patient-specific endpoints
- **Doctor Role**: Access to doctor-specific endpoints

## Setup Instructions

### Prerequisites

- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Database Setup

1. Update connection string in `appsettings.json`
2. Run Entity Framework migrations:

```bash
dotnet ef database update
```

### Running the Application

1. Clone the repository
2. Navigate to the APIs folder:

```bash
cd RIS/APIs
```

3. Run the application:

```bash
dotnet run
```

4. Access Swagger UI at `https://localhost:7099/swagger`

## Development Guidelines

### Adding New Features

1. Define DTOs in `Core/DTOs`
2. Create/Update domain models in `Core/Models`
3. Define interface in `Core/Interfaces`
4. Implement service in `APIs/Services`
5. Create controller in `APIs/Controllers`

### Error Handling

- Use try-catch blocks in controllers
- Return appropriate HTTP status codes
- Log errors appropriately

### Security

- Always validate input data
- Use role-based authorization
- Implement rate limiting for APIs
- Secure sensitive data

### Best Practices

- Follow SOLID principles
- Use async/await for I/O operations
- Implement proper validation
- Write unit tests for new features
- Document API changes
