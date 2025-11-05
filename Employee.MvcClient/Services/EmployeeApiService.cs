using System.Net.Http;
using System.Net.Http.Json;
using Employee.MvcClient.Models;

namespace Employee.MvcClient.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;
        public EmployeeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee.MvcClient.Models.Employee>> GetAllAsync()
        {
            var employees = await _httpClient.GetFromJsonAsync<List<Employee.MvcClient.Models.Employee>>("api/Employees");
            return employees ?? new List<Employee.MvcClient.Models.Employee>();
        }

    }
}
