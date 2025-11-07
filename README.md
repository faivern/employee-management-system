# Employee Management System

This is a simple employee management system built with ASP.NET Core. It consists of a backend API and an MVC client.

## Problem Solved

The application allows users to perform CRUD operations (Create, Read, Update, Delete) on employees and departments. It also provides a simple authentication system with a hardcoded admin user.

## Setup

### Backend

1.  **Database**: The backend uses SQL Server. The connection string is configured in `EmployeeManager.Backend/appsettings.json`. The default connection string is `Server=localhost\SQLEXPRESS;Database=EmployeeManagerDb;Trusted_Connection=True;TrustServerCertificate=True;`. You may need to update this to match your SQL Server instance.
2.  **Database Migrations**: To create the database and tables, run the following commands in the `EmployeeManager.Backend` directory:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

### Frontend

The MVC client is configured to connect to the backend at `https://localhost:7224/`. This is configured in `Employee.MvcClient/appsettings.json`.

## Running the Application

You need to run both the backend and the frontend simultaneously.

### Backend

Navigate to the `EmployeeManager.Backend` directory and run the following command:

```bash
dotnet run
```

The backend will be running on `https://localhost:7224`.

### Frontend

Navigate to the `Employee.MvcClient` directory and run the following command:

```bash
dotnet run
```

The frontend will be running on `https://localhost:7134`. You can access the application in your browser at this address.

### Admin Login

*   **Username**: admin
*   **Password**: 123
