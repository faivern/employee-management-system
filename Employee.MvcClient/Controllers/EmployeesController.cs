using Microsoft.AspNetCore.Mvc;
using Employee.MvcClient.Models;
using Employee.MvcClient.Services;

namespace Employee.MvcClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeApiService _employeeApiService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(EmployeeApiService employeeApiService, ILogger<EmployeesController> logger)
        {
            _employeeApiService = employeeApiService;
            _logger = logger;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Loading employee index page");
            var employees = await _employeeApiService.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} employees", employees?.Count ?? 0);
            return View(employees);
        }


    }
}
