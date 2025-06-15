# Employee API Documentation

This document provides detailed documentation for the Employee API endpoints in the CQRS-Without-MediatR project.

## Base URL
```
https://localhost:7xxx/api/employee
```

## Overview

The Employee API provides full CRUD (Create, Read, Update, Delete) operations for managing employee data. It follows RESTful conventions and implements the CQRS pattern for optimal performance and maintainability.

## Endpoints

### 1. Get All Employees

**Endpoint**: `GET /api/employee`

**Description**: Retrieves a list of all employees in the system.

**Request**: No parameters required

**Response**: Array of Employee objects

**Status Codes**:
- `200 OK`: Success
- `500 Internal Server Error`: Server error

**Example Response**:
```json
[
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
    "age": 33,
    "yearsOfService": 4,
    "createdAt": "2023-01-01T00:00:00Z",
    "updatedAt": null
  },
  {
    "employeeID": 2,
    "firstName": "Jane",
    "lastName": "Smith",
    "fullName": "Jane Smith",
    "gender": "Female",
    "genderDisplay": "Female",
    "dob": "1985-08-22",
    "emailID": "jane.smith@company.com",
    "phoneNo": "+1-555-0102",
    "doj": "2019-03-10",
    "age": 38,
    "yearsOfService": 5,
    "createdAt": "2023-01-01T00:00:00Z",
    "updatedAt": "2023-06-15T10:30:00Z"
  }
]
```

### 2. Get Employee by ID

**Endpoint**: `GET /api/employee/{id}`

**Description**: Retrieves a specific employee by their ID.

**Parameters**:
- `id` (path, required): The unique identifier of the employee

**Response**: Employee object

**Status Codes**:
- `200 OK`: Employee found
- `404 Not Found`: Employee not found
- `500 Internal Server Error`: Server error

**Example Request**:
```
GET /api/employee/1
```

**Example Response**:
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
  "age": 33,
  "yearsOfService": 4,
  "createdAt": "2023-01-01T00:00:00Z",
  "updatedAt": null
}
```

### 3. Create Employee

**Endpoint**: `POST /api/employee`

**Description**: Creates a new employee in the system.

**Request Body**: CreateEmployeeDto object

**Response**: Created Employee object with generated ID

**Status Codes**:
- `201 Created`: Employee created successfully
- `400 Bad Request`: Invalid input data
- `500 Internal Server Error`: Server error

**Request Body Schema**:
```json
{
  "firstName": "string (required, max 50 characters)",
  "lastName": "string (required, max 50 characters)",
  "gender": "enum (Male, Female, Other)",
  "dob": "date (required, format: YYYY-MM-DD)",
  "emailID": "string (required, valid email format, max 100 characters)",
  "phoneNo": "string (required, max 20 characters)",
  "doj": "date (required, format: YYYY-MM-DD)"
}
```

**Example Request**:
```json
{
  "firstName": "Alice",
  "lastName": "Johnson",
  "gender": "Female",
  "dob": "1992-12-03",
  "emailID": "alice.johnson@company.com",
  "phoneNo": "+1-555-0103",
  "doj": "2021-06-01"
}
```

**Example Response**:
```json
{
  "employeeID": 3,
  "firstName": "Alice",
  "lastName": "Johnson",
  "fullName": "Alice Johnson",
  "gender": "Female",
  "genderDisplay": "Female",
  "dob": "1992-12-03",
  "emailID": "alice.johnson@company.com",
  "phoneNo": "+1-555-0103",
  "doj": "2021-06-01",
  "age": 31,
  "yearsOfService": 2,
  "createdAt": "2023-12-15T14:30:00Z",
  "updatedAt": null
}
```

### 4. Update Employee

**Endpoint**: `PUT /api/employee/{id}`

**Description**: Updates an existing employee's information.

**Parameters**:
- `id` (path, required): The unique identifier of the employee to update

**Request Body**: UpdateEmployeeDto object

**Response**: Updated Employee object

**Status Codes**:
- `200 OK`: Employee updated successfully
- `400 Bad Request`: Invalid input data
- `404 Not Found`: Employee not found
- `500 Internal Server Error`: Server error

**Request Body Schema**: Same as Create Employee

**Example Request**:
```
PUT /api/employee/3
```

```json
{
  "firstName": "Alice",
  "lastName": "Johnson-Smith",
  "gender": "Female",
  "dob": "1992-12-03",
  "emailID": "alice.johnson-smith@company.com",
  "phoneNo": "+1-555-0103",
  "doj": "2021-06-01"
}
```

**Example Response**:
```json
{
  "employeeID": 3,
  "firstName": "Alice",
  "lastName": "Johnson-Smith",
  "fullName": "Alice Johnson-Smith",
  "gender": "Female",
  "genderDisplay": "Female",
  "dob": "1992-12-03",
  "emailID": "alice.johnson-smith@company.com",
  "phoneNo": "+1-555-0103",
  "doj": "2021-06-01",
  "age": 31,
  "yearsOfService": 2,
  "createdAt": "2023-12-15T14:30:00Z",
  "updatedAt": "2023-12-15T16:45:00Z"
}
```

### 5. Delete Employee

**Endpoint**: `DELETE /api/employee/{id}`

**Description**: Deletes an employee from the system.

**Parameters**:
- `id` (path, required): The unique identifier of the employee to delete

**Response**: Boolean indicating success

**Status Codes**:
- `200 OK`: Employee deleted successfully
- `404 Not Found`: Employee not found
- `500 Internal Server Error`: Server error

**Example Request**:
```
DELETE /api/employee/3
```

**Example Response**:
```json
true
```

## Data Models

### Employee (Response DTO)
```typescript
{
  employeeID: number;           // Unique identifier
  firstName: string;            // Employee's first name
  lastName: string;             // Employee's last name
  fullName: string;             // Computed: firstName + lastName
  gender: "Male" | "Female" | "Other"; // Gender enum
  genderDisplay: string;        // Human-readable gender
  dob: string;                  // Date of birth (ISO date)
  emailID: string;              // Email address
  phoneNo: string;              // Phone number
  doj: string;                  // Date of joining (ISO date)
  age: number;                  // Computed: current age
  yearsOfService: number;       // Computed: years since joining
  createdAt: string;            // Record creation timestamp
  updatedAt: string | null;     // Last update timestamp
}
```

### CreateEmployeeDto (Request DTO)
```typescript
{
  firstName: string;            // Required, max 50 chars
  lastName: string;             // Required, max 50 chars
  gender: "Male" | "Female" | "Other"; // Required
  dob: string;                  // Required, ISO date format
  emailID: string;              // Required, valid email, max 100 chars
  phoneNo: string;              // Required, max 20 chars
  doj: string;                  // Required, ISO date format
}
```

### UpdateEmployeeDto (Request DTO)
Same as CreateEmployeeDto - all fields are required for updates.

## Validation Rules

### First Name & Last Name
- Required
- Maximum 50 characters
- Cannot be empty or whitespace

### Gender
- Required
- Must be one of: "Male", "Female", "Other"

### Date of Birth (DOB)
- Required
- Must be a valid date
- Should be in the past
- Format: YYYY-MM-DD

### Email ID
- Required
- Must be a valid email format
- Maximum 100 characters
- Must be unique across all employees

### Phone Number
- Required
- Maximum 20 characters
- Should contain only digits, spaces, hyphens, parentheses, and plus sign

### Date of Joining (DOJ)
- Required
- Must be a valid date
- Should not be in the future
- Format: YYYY-MM-DD

## Error Responses

### Validation Error (400 Bad Request)
```json
{
  "type": "ValidationException",
  "message": "Validation failed",
  "errors": [
    {
      "field": "EmailID",
      "message": "Email address is required"
    },
    {
      "field": "FirstName",
      "message": "First name cannot exceed 50 characters"
    }
  ]
}
```

### Not Found Error (404 Not Found)
```json
{
  "type": "NotFoundException",
  "message": "Employee with ID 999 not found"
}
```

### Server Error (500 Internal Server Error)
```json
{
  "type": "InternalServerError",
  "message": "An unexpected error occurred"
}
```

## Example Usage with curl

### Get all employees
```bash
curl -X GET "https://localhost:7xxx/api/employee" \
  -H "Accept: application/json"
```

### Get employee by ID
```bash
curl -X GET "https://localhost:7xxx/api/employee/1" \
  -H "Accept: application/json"
```

### Create new employee
```bash
curl -X POST "https://localhost:7xxx/api/employee" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "firstName": "Bob",
    "lastName": "Wilson",
    "gender": "Male",
    "dob": "1988-03-15",
    "emailID": "bob.wilson@company.com",
    "phoneNo": "+1-555-0104",
    "doj": "2022-01-10"
  }'
```

### Update employee
```bash
curl -X PUT "https://localhost:7xxx/api/employee/1" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "firstName": "John",
    "lastName": "Doe-Updated",
    "gender": "Male",
    "dob": "1990-05-15",
    "emailID": "john.doe.updated@company.com",
    "phoneNo": "+1-555-0101",
    "doj": "2020-01-15"
  }'
```

### Delete employee
```bash
curl -X DELETE "https://localhost:7xxx/api/employee/1" \
  -H "Accept: application/json"
```

## Testing

Use the built-in Swagger UI for interactive testing:
1. Run the application
2. Navigate to `https://localhost:7xxx`
3. Use the interactive interface to test all endpoints

The Swagger UI provides:
- Request/response examples
- Parameter validation
- Real-time testing capabilities
- Comprehensive API documentation