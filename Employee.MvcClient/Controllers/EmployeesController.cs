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

    }
}
