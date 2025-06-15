-- CQRS Database Creation Script
-- This script creates the database and tables manually if EF migrations are not used

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CQRSDb')
BEGIN
    CREATE DATABASE CQRSDb;
    PRINT 'Database CQRSDb created successfully.';
END
ELSE
BEGIN
    PRINT 'Database CQRSDb already exists.';
END
GO

USE CQRSDb;
GO

-- Create Employees table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
BEGIN
    CREATE TABLE Employees (
        EmployeeID int IDENTITY(1,1) PRIMARY KEY,
        FirstName nvarchar(50) NOT NULL,
        LastName nvarchar(50) NOT NULL,
        Gender nvarchar(10) NOT NULL,
        DOB date NOT NULL,
        EmailID nvarchar(100) NOT NULL UNIQUE,
        PhoneNo nvarchar(20) NOT NULL,
        DOJ date NOT NULL,
        CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt datetime2 NULL,
        CreatedBy nvarchar(100) NULL,
        UpdatedBy nvarchar(100) NULL
    );
    
    -- Create indexes for better performance
    CREATE INDEX IX_Employees_EmailID ON Employees(EmailID);
    CREATE INDEX IX_Employees_LastName_FirstName ON Employees(LastName, FirstName);
    
    PRINT 'Employees table created successfully.';
END
ELSE
BEGIN
    PRINT 'Employees table already exists.';
END
GO

-- Create WeatherForecasts table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='WeatherForecasts' AND xtype='U')
BEGIN
    CREATE TABLE WeatherForecasts (
        Id int IDENTITY(1,1) PRIMARY KEY,
        Date date NOT NULL,
        TemperatureC int NOT NULL,
        Summary nvarchar(100) NULL,
        CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt datetime2 NULL,
        CreatedBy nvarchar(100) NULL,
        UpdatedBy nvarchar(100) NULL
    );
    
    -- Create index for better performance
    CREATE INDEX IX_WeatherForecasts_Date ON WeatherForecasts(Date);
    
    PRINT 'WeatherForecasts table created successfully.';
END
ELSE
BEGIN
    PRINT 'WeatherForecasts table already exists.';
END
GO

-- Insert sample data into Employees table
IF NOT EXISTS (SELECT 1 FROM Employees)
BEGIN
    INSERT INTO Employees (FirstName, LastName, Gender, DOB, EmailID, PhoneNo, DOJ, CreatedAt)
    VALUES 
        ('John', 'Doe', 'Male', '1990-05-15', 'john.doe@company.com', '+1-555-0101', '2020-01-15', GETUTCDATE()),
        ('Jane', 'Smith', 'Female', '1985-08-22', 'jane.smith@company.com', '+1-555-0102', '2019-03-10', GETUTCDATE()),
        ('Michael', 'Johnson', 'Male', '1992-12-03', 'michael.johnson@company.com', '+1-555-0103', '2021-06-01', GETUTCDATE()),
        ('Sarah', 'Williams', 'Female', '1988-04-18', 'sarah.williams@company.com', '+1-555-0104', '2018-09-05', GETUTCDATE()),
        ('David', 'Brown', 'Male', '1995-07-11', 'david.brown@company.com', '+1-555-0105', '2022-02-14', GETUTCDATE());
    
    PRINT 'Sample employee data inserted successfully.';
END
ELSE
BEGIN
    PRINT 'Employee data already exists.';
END
GO

-- Insert sample data into WeatherForecasts table
IF NOT EXISTS (SELECT 1 FROM WeatherForecasts)
BEGIN
    INSERT INTO WeatherForecasts (Date, TemperatureC, Summary, CreatedAt)
    VALUES 
        (DATEADD(day, 1, GETDATE()), 15, 'Cool', GETUTCDATE()),
        (DATEADD(day, 2, GETDATE()), 22, 'Mild', GETUTCDATE()),
        (DATEADD(day, 3, GETDATE()), 28, 'Warm', GETUTCDATE()),
        (DATEADD(day, 4, GETDATE()), 35, 'Hot', GETUTCDATE()),
        (DATEADD(day, 5, GETDATE()), 10, 'Chilly', GETUTCDATE());
    
    PRINT 'Sample weather forecast data inserted successfully.';
END
ELSE
BEGIN
    PRINT 'Weather forecast data already exists.';
END
GO

-- Verify the setup
SELECT 'Employees' as TableName, COUNT(*) as RecordCount FROM Employees
UNION ALL
SELECT 'WeatherForecasts' as TableName, COUNT(*) as RecordCount FROM WeatherForecasts;

PRINT 'Database setup completed successfully!';
GO