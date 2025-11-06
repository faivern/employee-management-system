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
        public async Task<IActionResult> Index(int? searchId)
        {
            if (searchId.HasValue)
            {
                var employee = await _employeeApiService.GetByIdAsync(searchId.Value);
                if (employee != null)
                {
                    return View(new List<Employee.MvcClient.Models.Employee> { employee });
                }

                return View(new List<Employee.MvcClient.Models.Employee>());

            }

            var employees = await _employeeApiService.GetAllAsync();
            return View(employees);
            
        }

        // GET: Employees/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();

            }
}
        }
}
