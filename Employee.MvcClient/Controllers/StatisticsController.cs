using Microsoft.AspNetCore.Mvc;
using Employee.MvcClient.Services;
using System.Threading.Tasks;
using System.Linq;

namespace Employee.MvcClient.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly EmployeeApiService _employeeApiService;
        private readonly DepartmentApiService _departmentApiService;

        public StatisticsController(EmployeeApiService employeeApiService, DepartmentApiService departmentApiService)
        {
            _employeeApiService = employeeApiService;
            _departmentApiService = departmentApiService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch all employees and departments from API
            var employees = await _employeeApiService.GetAllAsync();
            var departments = await _departmentApiService.GetAllAsync();
            var employeesWithSalary = employees.Where(e => e.Salary.HasValue).ToList();

            // === BASIC STATISTICS ===
            var totalEmployees = employees.Count;
            var totalDepartments = departments.Count;

            // === SALARY ANALYTICS ===
            var averageSalary = employeesWithSalary.Any()
                ? employeesWithSalary.Average(e => e.Salary!.Value)
                : 0;

            var highestSalary = employeesWithSalary.Any()
                ? employeesWithSalary.Max(e => e.Salary!.Value)
                : 0;

            var lowestSalary = employeesWithSalary.Any()
                ? employeesWithSalary.Min(e => e.Salary!.Value)
                : 0;

            var medianSalary = CalculateMedian(employeesWithSalary.Select(e => e.Salary!.Value).ToList());

            var salaryRange = highestSalary - lowestSalary;

            var totalPayrollCost = employeesWithSalary.Sum(e => e.Salary!.Value);

            // === DEPARTMENT ANALYTICS ===
            var departmentStats = employees
                .GroupBy(e => e.DepartmentId)
                .Select(g => new
                {
                    DepartmentId = g.Key,
                    DepartmentName = departments.FirstOrDefault(d => d.DepartmentId == g.Key)?.Name ?? "Unknown",
                    DepartmentLocation = departments.FirstOrDefault(d => d.DepartmentId == g.Key)?.Location ?? "N/A",
                    EmployeeCount = g.Count(),
                    AverageSalary = g.Where(e => e.Salary.HasValue).Any()
                        ? g.Where(e => e.Salary.HasValue).Average(e => e.Salary!.Value)
                        : 0,
                    TotalSalaryCost = g.Where(e => e.Salary.HasValue).Sum(e => e.Salary!.Value),
                    HighestSalary = g.Where(e => e.Salary.HasValue).Any()
                        ? g.Where(e => e.Salary.HasValue).Max(e => e.Salary!.Value)
                        : 0,
                    LowestSalary = g.Where(e => e.Salary.HasValue).Any()
                        ? g.Where(e => e.Salary.HasValue).Min(e => e.Salary!.Value)
                        : 0
                })
                .OrderByDescending(d => d.TotalSalaryCost)
                .ToList();

            // Highest and lowest paying departments
            var highestPayingDept = departmentStats.OrderByDescending(d => d.AverageSalary).FirstOrDefault();
            var lowestPayingDept = departmentStats.OrderBy(d => d.AverageSalary).FirstOrDefault();

            // === TOP EARNERS ===
            var topEarners = employeesWithSalary
                .OrderByDescending(e => e.Salary)
                .Take(5)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Salary,
                    YearlySalary = e.Salary!.Value * 12,
                    DepartmentName = departments.FirstOrDefault(d => d.DepartmentId == e.DepartmentId)?.Name ?? "Unknown"
                })
                .ToList();

            // Pass all statistics to ViewBag
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalDepartments = totalDepartments;
            ViewBag.AverageSalary = averageSalary;
            ViewBag.MedianSalary = medianSalary;
            ViewBag.HighestSalary = highestSalary;
            ViewBag.LowestSalary = lowestSalary;
            ViewBag.SalaryRange = salaryRange;
            ViewBag.TotalPayrollCost = totalPayrollCost;

            ViewBag.DepartmentStats = departmentStats;
            ViewBag.HighestPayingDept = highestPayingDept;
            ViewBag.LowestPayingDept = lowestPayingDept;

            ViewBag.TopEarners = topEarners;

            return View();
        }

        // Helper method to calculate median
        private decimal CalculateMedian(List<decimal> values)
        {
            if (!values.Any()) return 0;

            var sortedValues = values.OrderBy(v => v).ToList();
            int count = sortedValues.Count;

            if (count % 2 == 0)
            {
                // Even number of elements - average of two middle values
                return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2;
            }
            else
            {
                // Odd number of elements - middle value
                return sortedValues[count / 2];
            }
        }
    }
}
