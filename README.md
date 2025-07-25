# promode-assignment
An ASP.NET Core MVC web application for managing employee records. It includes functionality for creating, editing, deleting, searching, and paginating employee data.

---

##  Project Setup

Follow these steps to set up and run the project on your local machine.

### 1. Clone the Repository

```bash
git clone  https://github.com/thilinisewwandi416/promode-assignment.git
cd promode-assignment

Make sure you have the following installed:

    .NET 6 SDK or later

    SQL Server

    (Optional) Visual Studio 2022+ with ASP.NET and EF Core components


Step 1: Open appsettings.json
Find the file in the root directory of the project and update the connection string. 

    "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }


After that execute the included sql scripts to create the required Stored Procedures and the Table in below order.
    create_table.sql
    sp_AddEmployee.sql
    sp_DeleteEmployee.sql
    sp_GetEmployees.sql
    sp_UpdateEmployee.sql



