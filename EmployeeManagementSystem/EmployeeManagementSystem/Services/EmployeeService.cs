using System.Data;
using EmployeeManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeServiceRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var parameters = new[]
            {
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@Email", employee.Email),
                new SqlParameter("@PhoneNumber", employee.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@Department", employee.Department ?? (object)DBNull.Value),
                new SqlParameter("@EmployeeID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AddEmployee @FirstName, @LastName, @Email, @PhoneNumber, @Department, @EmployeeID OUTPUT",
                    parameters);

                int newEmployeeId = (int)parameters[5].Value;

                var newEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeID == newEmployeeId);

                if (newEmployee == null)
                    throw new Exception("Failed to retrieve the newly added employee.");

                return newEmployee;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding employee", ex);
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee updatedEmployee)
        {
            var parameters = new[]
            {
                new SqlParameter("@EmployeeID", id),
                new SqlParameter("@FirstName", updatedEmployee.FirstName),
                new SqlParameter("@LastName", updatedEmployee.LastName),
                new SqlParameter("@Email", updatedEmployee.Email),
                new SqlParameter("@PhoneNumber", updatedEmployee.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@Department", updatedEmployee.Department ?? (object)DBNull.Value)
            };

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateEmployee @EmployeeID, @FirstName, @LastName, @Email, @PhoneNumber, @Department",
                    parameters);

                var updatedEmployeeResult = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeID == id);

                if (updatedEmployeeResult == null)
                    throw new Exception("Failed to retrieve the updated employee.");

                return updatedEmployeeResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating employee", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var parameter = new SqlParameter("@EmployeeID", id);

            try
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_DeleteEmployee @EmployeeID",
                    parameter);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting employee", ex);
            }
        }

        public async Task<(List<Employee> Employees, int TotalCount)> GetEmployeesAsync(int pageNumber, int pageSize, string searchName = null, string searchDepartment = null)
        {
            var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var parameters = new[]
            {
                new SqlParameter("@PageNumber", pageNumber),
                new SqlParameter("@PageSize", pageSize),
                totalCountParam,
                new SqlParameter("@SearchName", (object)searchName ?? DBNull.Value),
                new SqlParameter("@SearchDepartment", (object)searchDepartment ?? DBNull.Value)
            };

            try
            {
                var employees = await _context.Employees
                    .FromSqlRaw("EXEC sp_GetEmployees @PageNumber, @PageSize, @TotalCount OUTPUT, @SearchName, @SearchDepartment", parameters)
                    .ToListAsync();

                int totalCount = (int)totalCountParam.Value;
                return (employees, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees", ex);
            }
        }
    }
}