@echo off
echo Setting up CQRS Database...

REM Get the directory where this script is located
set SCRIPT_DIR=%~dp0
REM Navigate to project root (two levels up from scripts\database)
cd /d "%SCRIPT_DIR%..\.."

echo Project root: %CD%

cd src\CQRS.Api

echo Creating initial migration...
dotnet ef migrations add InitialCreate --project ..\CQRS.Infrastructure --startup-project .
if %errorlevel% neq 0 (
    echo Error creating migration
    cd /d "%SCRIPT_DIR%..\.."
    pause
    exit /b 1
)

echo Updating database...
dotnet ef database update --project ..\CQRS.Infrastructure --startup-project .
if %errorlevel% neq 0 (
    echo Error updating database
    cd /d "%SCRIPT_DIR%..\.."
    pause
    exit /b 1
)

echo Database setup completed!
echo You can now run the application with: dotnet run

REM Navigate back to project root
cd /d "%SCRIPT_DIR%..\.."
pause