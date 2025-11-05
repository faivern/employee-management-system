using Microsoft.AspNetCore.Mvc;
using Employee.MvcClient.Models;
using Employee.MvcClient.Services;

namespace Employee.MvcClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeApiService _employeeApiService;
        public EmployeesController(EmployeeApiService employeeApiService)
        {
            _employeeApiService = employeeApiService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeApiService.GetAllAsync();
            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee.MvcClient.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                var success = await _employeeApiService.CreateAsync(employee);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create employee");
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeApiService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee.MvcClient.Models.Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var success = await _employeeApiService.UpdateAsync(id, employee);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update employee");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeApiService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _employeeApiService.DeleteAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete), new { id });
        }

        // POST: Employees/Logout
        public IActionResult Logout()
        {
            // Add your logout logic here (e.g., clear session, cookies, etc.)
            return RedirectToAction("Index", "Home");
        }

    }
}
