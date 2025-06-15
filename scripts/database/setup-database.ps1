# PowerShell script to set up the database for CQRS project

Write-Host "Setting up CQRS Database..." -ForegroundColor Green

# Get the script directory and navigate to project root
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent (Split-Path -Parent $scriptDir)
Set-Location $projectRoot

Write-Host "Project root: $projectRoot" -ForegroundColor Cyan

# Navigate to the API project directory (startup project)
Set-Location "src\CQRS.Api"

Write-Host "Creating initial migration..." -ForegroundColor Yellow
try {
    # Run migration command with Infrastructure project as target
    dotnet ef migrations add InitialCreate --project ..\CQRS.Infrastructure --startup-project .
    Write-Host "Migration created successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error creating migration: $_" -ForegroundColor Red
    Set-Location $projectRoot
    exit 1
}

Write-Host "Updating database..." -ForegroundColor Yellow
try {
    # Run database update with Infrastructure project as target
    dotnet ef database update --project ..\CQRS.Infrastructure --startup-project .
    Write-Host "Database updated successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error updating database: $_" -ForegroundColor Red
    Set-Location $projectRoot
    exit 1
}

Write-Host "Database setup completed!" -ForegroundColor Green
Write-Host "You can now run the application with: dotnet run" -ForegroundColor Cyan

# Navigate back to project root
Set-Location $projectRoot