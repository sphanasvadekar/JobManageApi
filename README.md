# JobManageApi
API Developed for Job Management

Job Management API
The Job Management API enables users to perform various operations related to job management, location management, and department management. Key functionalities include creating, updating, and retrieving jobs, as well as managing locations and departments.

Job Management:
Create Job: Create a new job with specified details.
Update Job: Update an existing job with new information.
Get All Jobs: Retrieve a list of all available jobs.
Location Management:
Create Location: Add a new location to the system.
Update Location: Update details of an existing location.
Get All Locations: Retrieve a list of all available locations.
Department Management:
Create Department: Add a new department to the system.
Update Department: Update details of an existing department.
Get All Departments: Retrieve a list of all available departments.
Search Filter:
Users can utilize search filters to find specific jobs based on various criteria.

Technology Stack:
Framework: .NET Core
ORM: Entity Framework Core (Code First Approach)
Database: SQL Server
Software Requirements:
Development Environment: Visual Studio 2022 and above.
NuGet Packages:
Microsoft.EntityFrameworkCore (8.0.4)
Microsoft.EntityFrameworkCore.SqlServer (8.0.4)
Microsoft.EntityFrameworkCore.Tools (8.0.4)
Swashbuckle.AspNetCore (6.4.0)
Setup Instructions:
Database Setup:

Execute the SQL script provided in the "SqlScript" folder.
Modify the app settings file to configure the connection string with your server. Replace "Server=IN-5CD9370XKC" with "Server={YourServerName};Database=JobManagementDb;Trusted_connection=true;TrustServerCertificate=true;".
Authentication:

Use the following credentials for authentication:
Username: "User"
Password: "Test@123"
Initial Setup:

Begin by creating locations and departments since their IDs are required when creating jobs.
Creating Jobs:

After setting up locations and departments, create jobs by specifying the location ID and department ID.
Postman Collection:

Explore the provided Postman collection in the "PostmanCollection" folder for easy testing and interaction with the API endpoints.
