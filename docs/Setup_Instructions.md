# Setup Instructions

This document provides comprehensive setup instructions for the CQRS-Without-MediatR project.

## Prerequisites

- .NET 8.0 SDK
- SQL Server LocalDB or SQL Server Express
- Visual Studio 2022 or VS Code
- Git

## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CQRS-Without-MeditR
   ```

2. **Run the database setup script**
   ```powershell
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
   - Swagger UI: `https://localhost:7xxx` (port will be shown in console)
   - API Base URL: `https://localhost:7xxx/api`

## Detailed Setup

### Database Configuration

The application uses SQL Server LocalDB by default. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CQRSDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Manual Database Setup

If the automated scripts don't work, you can set up the database manually:

1. **Navigate to the API project**
   ```bash
   cd src\CQRS.Api
   ```

2. **Install EF Core tools (if not already installed)**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Create and run migrations**
   ```bash
   # Create migration
   dotnet ef migrations add InitialCreate --project ..\CQRS.Infrastructure --startup-project .
   
   # Update database
   dotnet ef database update --project ..\CQRS.Infrastructure --startup-project .
   ```

### Project Structure

The project follows Clean Architecture principles with the following layers:

- **CQRS.Api**: Web API layer with controllers and middleware
- **CQRS.Application**: Business logic layer with CQRS handlers
- **CQRS.Domain**: Core domain entities and interfaces
- **CQRS.Infrastructure**: Data access and external services
- **CQRS.Shared**: Common utilities and extensions

### Development Workflow

1. **Feature Development**: Follow the feature-based organization in the Application layer
2. **Database Changes**: Create migrations for any entity changes
3. **Testing**: Run unit and integration tests before committing
4. **API Documentation**: Update Swagger documentation for new endpoints

### Troubleshooting

#### Common Issues

1. **Migration Errors**: Ensure you're running commands from the correct directory
2. **Connection Issues**: Verify SQL Server LocalDB is installed and running
3. **Port Conflicts**: Check if the configured ports are available

#### Getting Help

- Check the existing documentation in the `docs/` folder
- Review the implementation summary for architecture details
- Consult the migration guide for upgrade instructions

## Next Steps

After successful setup:

1. Explore the API endpoints using Swagger UI
2. Review the Employee and WeatherForecast features
3. Study the CQRS implementation patterns
4. Consider adding new features following the established structure