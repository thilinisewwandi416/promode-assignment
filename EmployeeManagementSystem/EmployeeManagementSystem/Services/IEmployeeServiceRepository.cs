using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeServiceRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(int id, Employee updatedEmployee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<(List<Employee> Employees, int TotalCount)> GetEmployeesAsync(int pageNumber, int pageSize, string searchName = null, string searchDepartment = null);

    }
}
