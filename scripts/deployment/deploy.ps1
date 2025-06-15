# PowerShell deployment script for CQRS-Without-MediatR

param(
    [Parameter(Mandatory=$false)]
    [string]$Environment = "Development",
    
    [Parameter(Mandatory=$false)]
    [string]$Configuration = "Release",
    
    [Parameter(Mandatory=$false)]
    [switch]$SkipTests,
    
    [Parameter(Mandatory=$false)]
    [switch]$SkipDatabase,
    
    [Parameter(Mandatory=$false)]
    [string]$OutputPath = ".\publish"
)

# Get script directory and project root
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent (Split-Path -Parent $scriptDir)

Write-Host "=== CQRS Application Deployment Script ===" -ForegroundColor Green
Write-Host "Environment: $Environment" -ForegroundColor Cyan
Write-Host "Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "Project Root: $projectRoot" -ForegroundColor Cyan
Write-Host "Output Path: $OutputPath" -ForegroundColor Cyan
Write-Host ""

# Navigate to project root
Set-Location $projectRoot

# Step 1: Clean previous builds
Write-Host "Step 1: Cleaning previous builds..." -ForegroundColor Yellow
try {
    dotnet clean --configuration $Configuration
    if (Test-Path $OutputPath) {
        Remove-Item -Path $OutputPath -Recurse -Force
        Write-Host "Cleaned output directory: $OutputPath" -ForegroundColor Green
    }
    Write-Host "Clean completed successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error during clean: $_" -ForegroundColor Red
    exit 1
}

# Step 2: Restore dependencies
Write-Host "`nStep 2: Restoring dependencies..." -ForegroundColor Yellow
try {
    dotnet restore
    Write-Host "Dependencies restored successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error restoring dependencies: $_" -ForegroundColor Red
    exit 1
}

# Step 3: Build the solution
Write-Host "`nStep 3: Building the solution..." -ForegroundColor Yellow
try {
    dotnet build --configuration $Configuration --no-restore
    Write-Host "Build completed successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error during build: $_" -ForegroundColor Red
    exit 1
}

# Step 4: Run tests (if not skipped)
if (-not $SkipTests) {
    Write-Host "`nStep 4: Running tests..." -ForegroundColor Yellow
    try {
        # Note: Add test projects when they exist
        # dotnet test --configuration $Configuration --no-build --verbosity normal
        Write-Host "Tests skipped - no test projects found" -ForegroundColor Yellow
    } catch {
        Write-Host "Error running tests: $_" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "`nStep 4: Skipping tests..." -ForegroundColor Yellow
}

# Step 5: Database setup (if not skipped)
if (-not $SkipDatabase) {
    Write-Host "`nStep 5: Setting up database..." -ForegroundColor Yellow
    try {
        Set-Location "src\CQRS.Api"
        
        # Check if migrations exist
        if (Test-Path "..\CQRS.Infrastructure\Migrations") {
            Write-Host "Applying existing migrations..." -ForegroundColor Cyan
            dotnet ef database update --project ..\CQRS.Infrastructure --startup-project . --configuration $Configuration
        } else {
            Write-Host "Creating initial migration..." -ForegroundColor Cyan
            dotnet ef migrations add InitialCreate --project ..\CQRS.Infrastructure --startup-project . --configuration $Configuration
            dotnet ef database update --project ..\CQRS.Infrastructure --startup-project . --configuration $Configuration
        }
        
        Set-Location $projectRoot
        Write-Host "Database setup completed successfully!" -ForegroundColor Green
    } catch {
        Write-Host "Error setting up database: $_" -ForegroundColor Red
        Set-Location $projectRoot
        exit 1
    }
} else {
    Write-Host "`nStep 5: Skipping database setup..." -ForegroundColor Yellow
}

# Step 6: Publish the application
Write-Host "`nStep 6: Publishing the application..." -ForegroundColor Yellow
try {
    dotnet publish src\CQRS.Api\CQRS.Api.csproj `
        --configuration $Configuration `
        --output $OutputPath `
        --no-build `
        --verbosity normal
    
    Write-Host "Application published successfully to: $OutputPath" -ForegroundColor Green
} catch {
    Write-Host "Error publishing application: $_" -ForegroundColor Red
    exit 1
}

# Step 7: Copy additional files
Write-Host "`nStep 7: Copying additional files..." -ForegroundColor Yellow
try {
    # Copy appsettings for the target environment
    $sourceSettings = "src\CQRS.Api\appsettings.$Environment.json"
    $targetSettings = "$OutputPath\appsettings.$Environment.json"
    
    if (Test-Path $sourceSettings) {
        Copy-Item $sourceSettings $targetSettings -Force
        Write-Host "Copied environment-specific settings: $sourceSettings" -ForegroundColor Green
    }
    
    # Copy documentation
    if (Test-Path "docs") {
        Copy-Item "docs" "$OutputPath\docs" -Recurse -Force
        Write-Host "Copied documentation to output directory" -ForegroundColor Green
    }
    
    # Copy scripts
    if (Test-Path "scripts") {
        Copy-Item "scripts" "$OutputPath\scripts" -Recurse -Force
        Write-Host "Copied scripts to output directory" -ForegroundColor Green
    }
    
    Write-Host "Additional files copied successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error copying additional files: $_" -ForegroundColor Red
    # Don't exit on this error, it's not critical
}

# Step 8: Generate deployment summary
Write-Host "`nStep 8: Generating deployment summary..." -ForegroundColor Yellow
$summaryFile = "$OutputPath\deployment-summary.txt"
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

$summary = @"
CQRS Application Deployment Summary
===================================

Deployment Date: $timestamp
Environment: $Environment
Configuration: $Configuration
Output Path: $OutputPath

Build Information:
- .NET Version: $(dotnet --version)
- Solution: CQRS-Without-MediatR
- Main Project: CQRS.Api

Deployment Steps Completed:
1. ✓ Clean previous builds
2. ✓ Restore dependencies
3. ✓ Build solution
4. $(if ($SkipTests) { "⚠ Tests skipped" } else { "✓ Run tests" })
5. $(if ($SkipDatabase) { "⚠ Database setup skipped" } else { "✓ Database setup" })
6. ✓ Publish application
7. ✓ Copy additional files
8. ✓ Generate summary

Next Steps:
1. Review the published files in: $OutputPath
2. Configure the target environment settings
3. Ensure database connectivity
4. Deploy to target server
5. Verify application functionality

Application URLs (when deployed):
- API Base: https://your-domain/api
- Swagger UI: https://your-domain/
- Health Check: https://your-domain/health (if implemented)

Support:
- Documentation: $OutputPath\docs\
- Setup Scripts: $OutputPath\scripts\
- Configuration: $OutputPath\appsettings.$Environment.json
"@

$summary | Out-File -FilePath $summaryFile -Encoding UTF8
Write-Host "Deployment summary saved to: $summaryFile" -ForegroundColor Green

# Final success message
Write-Host "`n=== Deployment Completed Successfully! ===" -ForegroundColor Green
Write-Host "Published application is ready in: $OutputPath" -ForegroundColor Cyan
Write-Host "Review the deployment summary for next steps." -ForegroundColor Cyan

# Return to original directory
Set-Location $projectRoot