# CQRS Without MediatR

A clean architecture implementation of CQRS (Command Query Responsibility Segregation) pattern without using MediatR, built with .NET 8 and Entity Framework Core.

## 🏗️ Architecture Overview

This project follows Clean Architecture principles with clear separation of concerns:

```
CQRS-Without-MediatR/
├── src/                          # Source code
│   ├── CQRS.Api/                # Presentation Layer (Controllers, Middleware)
│   ├── CQRS.Application/        # Application Layer (Business Logic, CQRS Handlers)
│   ├── CQRS.Domain/             # Domain Layer (Entities, Interfaces, Business Rules)
│   ├── CQRS.Infrastructure/     # Infrastructure Layer (Data Access, External Services)
│   └── CQRS.Shared/             # Shared Components (Constants, Extensions, Utilities)
├── docs/                        # Documentation
│   ├── API_Documentation.md     # Complete API reference
│   ├── Employee_API_Documentation.md # Employee API details
│   ├── Architecture_Overview.md # Architecture details
│   └── Setup_Instructions.md    # Setup guide
├── scripts/                     # Automation scripts
│   ├── database/               # Database setup scripts
│   │   ├── setup-database.ps1  # PowerShell setup
│   │   ├── setup-database.bat  # Batch setup
│   │   └── CreateDatabase.sql  # Manual SQL setup
│   └── deployment/             # Deployment scripts
│       └── deploy.ps1          # PowerShell deployment
└── tests/                      # Test projects (future)
```

## ✨ Features

- **Clean Architecture**: Well-organized layers with clear dependencies
- **CQRS Pattern**: Separate commands and queries for better scalability
- **Feature-Based Organization**: Related components grouped by business feature
- **Input Validation**: Comprehensive validation for all commands
- **Global Exception Handling**: Centralized error handling with proper HTTP responses
- **Entity Framework Core**: Code-first approach with migrations
- **SQL Server Support**: Optimized for SQL Server with retry policies
- **Swagger Documentation**: Auto-generated API documentation
- **Audit Trail**: Automatic tracking of created/updated timestamps
- **Transaction Support**: Unit of Work pattern with transaction management
- **Value Objects**: Domain-driven design with Email and PhoneNumber value objects
- **Domain Events**: Support for domain event handling

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CQRS-Without-MediatR
   ```

2. **Quick Database Setup (Recommended)**
   ```bash
   # For PowerShell users
   .\scripts\database\setup-database.ps1
   
   # For Command Prompt users
   .\scripts\database\setup-database.bat
   ```

3. **Run the application**
   ```bash
   cd src\CQRS.Api
   dotnet run
   ```

4. **Access the API**
   - Swagger UI: `https://localhost:7xxx` (port shown in console)
   - API Base URL: `https://localhost:7xxx/api`

> 📖 **Need detailed setup instructions?** See [Setup_Instructions.md](docs/Setup_Instructions.md)

## 📚 Documentation

- **[Setup Instructions](docs/Setup_Instructions.md)** - Comprehensive setup guide
- **[API Documentation](docs/API_Documentation.md)** - Complete API reference
- **[Employee API](docs/Employee_API_Documentation.md)** - Detailed Employee API docs
- **[Architecture Overview](docs/Architecture_Overview.md)** - Architecture details and design decisions

## 🔧 API Endpoints

### Employee Management
- `GET /api/employee` - Get all employees
- `GET /api/employee/{id}` - Get employee by ID
- `POST /api/employee` - Create new employee
- `PUT /api/employee/{id}` - Update employee
- `DELETE /api/employee/{id}` - Delete employee

### Weather Forecast Management
- `GET /api/weatherforecast` - Get all weather forecasts
- `GET /api/weatherforecast/{id}` - Get weather forecast by ID
- `POST /api/weatherforecast` - Create new weather forecast
- `PUT /api/weatherforecast/{id}` - Update weather forecast
- `DELETE /api/weatherforecast/{id}` - Delete weather forecast

### Example Usage

**Create Employee:**
```json
POST /api/employee
{
  "firstName": "John",
  "lastName": "Doe",
  "gender": "Male",
  "dob": "1990-05-15",
  "emailID": "john.doe@company.com",
  "phoneNo": "+1-555-0101",
  "doj": "2020-01-15"
}
```

**Response:**
```json
{
  "employeeID": 1,
  "firstName": "John",
  "lastName": "Doe",
  "fullName": "John Doe",
  "gender": "Male",
  "genderDisplay": "Male",
  "dob": "1990-05-15",
  "emailID": "john.doe@company.com",
  "phoneNo": "+1-555-0101",
  "doj": "2020-01-15",
  "age": 34,
  "yearsOfService": 4,
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": null
}
```

## 🏛️ Project Structure

### Domain Layer (`CQRS.Domain`)
- **Entities**: Core business entities with rich domain logic
- **Value Objects**: Immutable objects (Email, PhoneNumber)
- **Enums**: Domain-specific enumerations
- **Interfaces**: Repository contracts and domain services
- **Events**: Domain events for decoupled communication

### Application Layer (`CQRS.Application`)
- **Features**: Organized by business capabilities
  - **Commands**: Write operations (Create, Update, Delete)
  - **Queries**: Read operations (Get, List)
  - **DTOs**: Data transfer objects for API contracts
  - **Validators**: Input validation logic
- **Common**: Shared CQRS infrastructure
  - **Interfaces**: CQRS contracts
  - **Dispatchers**: Command and query dispatchers
  - **Exceptions**: Custom exception types
  - **Behaviors**: Cross-cutting concerns

### Infrastructure Layer (`CQRS.Infrastructure`)
- **Data**: Entity Framework DbContext and configurations
- **Repositories**: Data access implementations
- **Services**: External service implementations (Email, etc.)
- **Extensions**: Dependency injection setup

### API Layer (`CQRS.Api`)
- **Controllers**: HTTP endpoints
- **Middleware**: Cross-cutting concerns (exception handling, logging)
- **Extensions**: Service registration

### Shared Layer (`CQRS.Shared`)
- **Constants**: Application-wide constants
- **Extensions**: Utility extension methods
- **Utilities**: Helper classes (PasswordHasher, etc.)

## 🎯 Key Design Decisions

### 1. **No MediatR Dependency**
- Custom command/query dispatchers for better control
- Reduced external dependencies
- Clear understanding of CQRS implementation

### 2. **Feature-Based Organization**
- Related commands, queries, and handlers grouped together
- Easy to locate and maintain feature-specific code
- Supports team collaboration on different features

### 3. **Clean Architecture**
- Dependencies point inward toward the domain
- Business logic isolated from external concerns
- Easy to test and maintain

### 4. **Domain-Driven Design**
- Rich domain models with business logic
- Value objects for domain concepts
- Domain events for decoupled communication

### 5. **Comprehensive Validation**
- Input validation at the application layer
- Business rule validation in domain entities
- Consistent error responses

## 🛠️ Development

### Manual Database Setup

If the automated scripts don't work:

1. **Update connection string** in `src/CQRS.Api/appsettings.Development.json`
2. **Create migration:**
   ```bash
   cd src\CQRS.Api
   dotnet ef migrations add InitialCreate --project ..\CQRS.Infrastructure --startup-project .
   ```
3. **Update database:**
   ```bash
   dotnet ef database update --project ..\CQRS.Infrastructure --startup-project .
   ```

### Adding New Features

Follow the established patterns in the `Features` folder. Each feature should have:
- Commands (for write operations)
- Queries (for read operations)  
- DTOs (for data transfer)
- Handlers (for business logic)
- Validators (for input validation)

### Deployment

Use the deployment script for automated deployment:

```bash
# Basic deployment
.\scripts\deployment\deploy.ps1

# Production deployment
.\scripts\deployment\deploy.ps1 -Environment Production -Configuration Release

# Skip tests and database setup
.\scripts\deployment\deploy.ps1 -SkipTests -SkipDatabase
```

## 🆘 Troubleshooting

- **Database Issues**: See [Setup_Instructions.md](docs/Setup_Instructions.md)
- **API Issues**: Check [API_Documentation.md](docs/API_Documentation.md)
- **Architecture Questions**: Review [Architecture_Overview.md](docs/Architecture_Overview.md)

## 🔗 Related Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Domain-Driven Design](https://domainlanguage.com/ddd/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)