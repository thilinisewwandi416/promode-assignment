using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServiceRepository _employeeService;
        private readonly EmployeeDbContext _context;

        public EmployeeController(IEmployeeServiceRepository employeeService, EmployeeDbContext context)
        {
            _employeeService = employeeService;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var (employees, totalCount) = await _employeeService.GetEmployeesAsync(pageNumber, pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.AddEmployeeAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error adding employee: {ex.Message}");
                }
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employee);
                    if (updatedEmployee == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating employee: {ex.Message}");
                }
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _employeeService.DeleteEmployeeAsync(id);
                if (!success)
                {
                    TempData["ErrorMessage"] = "Failed to delete employee. The employee may not exist.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting employee: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}