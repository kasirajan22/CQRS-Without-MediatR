# API Documentation

This document provides comprehensive API documentation for the CQRS-Without-MediatR project.

## Base URL

```
https://localhost:7xxx/api
```

*Note: The exact port number will be displayed when you run the application.*

## Authentication

Currently, the API does not require authentication. All endpoints are publicly accessible.

## Common Response Formats

### Success Response
```json
{
  "data": { ... },
  "success": true
}
```

### Error Response
```json
{
  "error": "Error message",
  "success": false
}
```

## Employee API

### Get All Employees
- **Endpoint**: `GET /api/employee`
- **Description**: Retrieves all employees
- **Response**: Array of Employee objects

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
  }
]
```

### Get Employee by ID
- **Endpoint**: `GET /api/employee/{id}`
- **Description**: Retrieves a specific employee by ID
- **Parameters**: 
  - `id` (path): Employee ID
- **Response**: Employee object

### Create Employee
- **Endpoint**: `POST /api/employee`
- **Description**: Creates a new employee
- **Request Body**:

```json
{
  "firstName": "Jane",
  "lastName": "Smith",
  "gender": "Female",
  "dob": "1985-08-22",
  "emailID": "jane.smith@company.com",
  "phoneNo": "+1-555-0102",
  "doj": "2019-03-10"
}
```

- **Response**: Created Employee object with generated ID

### Update Employee
- **Endpoint**: `PUT /api/employee/{id}`
- **Description**: Updates an existing employee
- **Parameters**: 
  - `id` (path): Employee ID
- **Request Body**: Same as Create Employee
- **Response**: Updated Employee object

### Delete Employee
- **Endpoint**: `DELETE /api/employee/{id}`
- **Description**: Deletes an employee
- **Parameters**: 
  - `id` (path): Employee ID
- **Response**: Boolean indicating success

## Weather Forecast API

### Get All Weather Forecasts
- **Endpoint**: `GET /api/weatherforecast`
- **Description**: Retrieves all weather forecasts
- **Response**: Array of WeatherForecast objects

```json
[
  {
    "id": 1,
    "date": "2024-01-15",
    "temperatureC": 22,
    "temperatureF": 71,
    "summary": "Mild",
    "createdAt": "2023-01-01T00:00:00Z",
    "updatedAt": null
  }
]
```

### Get Weather Forecast by ID
- **Endpoint**: `GET /api/weatherforecast/{id}`
- **Description**: Retrieves a specific weather forecast by ID
- **Parameters**: 
  - `id` (path): Weather Forecast ID
- **Response**: WeatherForecast object

### Create Weather Forecast
- **Endpoint**: `POST /api/weatherforecast`
- **Description**: Creates a new weather forecast
- **Request Body**:

```json
{
  "date": "2024-01-15",
  "temperatureC": 22,
  "summary": "Mild"
}
```

- **Response**: Created WeatherForecast object with generated ID

### Update Weather Forecast
- **Endpoint**: `PUT /api/weatherforecast/{id}`
- **Description**: Updates an existing weather forecast
- **Parameters**: 
  - `id` (path): Weather Forecast ID
- **Request Body**: Same as Create Weather Forecast
- **Response**: Updated WeatherForecast object

### Delete Weather Forecast
- **Endpoint**: `DELETE /api/weatherforecast/{id}`
- **Description**: Deletes a weather forecast
- **Parameters**: 
  - `id` (path): Weather Forecast ID
- **Response**: Boolean indicating success

## Data Models

### Employee
```json
{
  "employeeID": "integer",
  "firstName": "string",
  "lastName": "string",
  "fullName": "string (computed)",
  "gender": "enum (Male, Female, Other)",
  "genderDisplay": "string",
  "dob": "date",
  "emailID": "string",
  "phoneNo": "string",
  "doj": "date",
  "age": "integer (computed)",
  "yearsOfService": "integer (computed)",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

### WeatherForecast
```json
{
  "id": "integer",
  "date": "date",
  "temperatureC": "integer",
  "temperatureF": "integer (computed)",
  "summary": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime?"
}
```

## Error Codes

- **400 Bad Request**: Invalid request data
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server error

## Rate Limiting

Currently, no rate limiting is implemented.

## Swagger UI

Interactive API documentation is available at the root URL when running in development mode:
```
https://localhost:7xxx/
```

## Testing

Use tools like Postman, curl, or the built-in Swagger UI to test the API endpoints.

### Example curl commands:

```bash
# Get all employees
curl -X GET "https://localhost:7xxx/api/employee"

# Create a new employee
curl -X POST "https://localhost:7xxx/api/employee" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Test",
    "lastName": "User",
    "gender": "Male",
    "dob": "1990-01-01",
    "emailID": "test@example.com",
    "phoneNo": "+1-555-0123",
    "doj": "2023-01-01"
  }'
```