using Microsoft.AspNetCore.Mvc;
using Employee.MvcClient.Models;
using Employee.MvcClient.Services;

namespace Employee.MvcClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeApiService _employeeApiService;
        private readonly DepartmentApiService _departmentApiService;

        public EmployeesController(EmployeeApiService employeeApiService, DepartmentApiService departmentApiService)
        {
            _employeeApiService = employeeApiService;
            _departmentApiService = departmentApiService;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? searchId)
        {
            List<Employee.MvcClient.Models.Employee> employees;

            if (searchId.HasValue)
            {
                var employee = await _employeeApiService.GetByIdAsync(searchId.Value);
                if (employee != null)
                {
                    employees = new List<Employee.MvcClient.Models.Employee> { employee };
                }
                else
                {
                    return View(new List<EmployeeViewModel>());
                }
            }
            else
            {
                employees = await _employeeApiService.GetAllAsync();
            }

            // Fetch all departments
            var departments = await _departmentApiService.GetAllAsync();
            var departmentDict = departments.ToDictionary(d => d.DepartmentId);

            // Create ViewModels combining employee and department data
            var employeeViewModels = employees.Select(e =>
            {
                var department = departmentDict.GetValueOrDefault(e.DepartmentId);
                return new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Salary = e.Salary,
                    DepartmentId = e.DepartmentId,
                    Name = department?.Name ?? "N/A",
                    Location = department?.Location ?? "N/A"
                };
            }).ToList();

            return View(employeeViewModels);
        }

        // GET: Employees/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentApiService.GetAllAsync();
            ViewBag.Departments = departments;
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee.MvcClient.Models.Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var created = await _employeeApiService.CreateAsync(employee);

            if (!created)
            {
                ModelState.AddModelError(string.Empty, "Error creating employee.");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeApiService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            var departments = await _departmentApiService.GetAllAsync();
            var department = departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
            
            var viewModel = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                Name = department?.Name ?? "N/A",
                Location = department?.Location ?? "N/A"
            };

            ViewBag.Departments = departments;
            return View(viewModel);
        }

        // POST: Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int EmployeeId, EmployeeViewModel viewModel)
        {
            if (EmployeeId != viewModel.EmployeeId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                var departments = await _departmentApiService.GetAllAsync();
                ViewBag.Departments = departments;
                return View(viewModel);
            }
            
            // Viewmodel to Employee
            var employee = new Employee.MvcClient.Models.Employee
            {
                EmployeeId = viewModel.EmployeeId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Salary = viewModel.Salary,
                DepartmentId = viewModel.DepartmentId
            };
            
            var updated = await _employeeApiService.UpdateAsync(EmployeeId, employee);
            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "Error updating employee.");
                var departments = await _departmentApiService.GetAllAsync();
                ViewBag.Departments = departments;
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Employees/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeApiService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
    
            var departments = await _departmentApiService.GetAllAsync();
            var department = departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
     
            var viewModel = new EmployeeViewModel
 {
     EmployeeId = employee.EmployeeId,
FirstName = employee.FirstName,
         LastName = employee.LastName,
      Email = employee.Email,
                Salary = employee.Salary,
      DepartmentId = employee.DepartmentId,
       Name = department?.Name ?? "N/A",
       Location = department?.Location ?? "N/A"
     };

      return View(viewModel);
     }

        // POST: Employees/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel viewModel)
        {
    var deleted = await _employeeApiService.DeleteAsync(viewModel.EmployeeId);

            if (!deleted)
      {
       ModelState.AddModelError(string.Empty, "Error deleting employee.");
                
  var departments = await _departmentApiService.GetAllAsync();
             var department = departments.FirstOrDefault(d => d.DepartmentId == viewModel.DepartmentId);
              viewModel.Name = department?.Name ?? "N/A";
            viewModel.Location = department?.Location ?? "N/A";
      
  return View(viewModel);
         }

        return RedirectToAction(nameof(Index));
        }
    }
}