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

        // GET: api/Employees
        public async Task<List<Employee.MvcClient.Models.Employee>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Employees");
            if (!response.IsSuccessStatusCode)
            {
                return new List<Employee.MvcClient.Models.Employee>();
            }

            var employees = await _httpClient.GetFromJsonAsync<List<Employee.MvcClient.Models.Employee>>("api/Employees");
            return employees ?? new List<Employee.MvcClient.Models.Employee>();
        }


        // GET: api/Employees/{id}
        public async Task<Employee.MvcClient.Models.Employee?> GetByIdAsync(int EmployeeId)
        {

            var response = await _httpClient.GetAsync($"api/Employees/{EmployeeId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var employee = await _httpClient.GetFromJsonAsync<Employee.MvcClient.Models.Employee>($"api/Employees/{EmployeeId}");
            return employee;
        }


        // POST: api/Employees
        public async Task<bool> CreateAsync(Employee.MvcClient.Models.Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employees", employee);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return response.IsSuccessStatusCode;
        }


        // Edit: api/Employees/{id}
        public async Task<bool> UpdateAsync(int EmployeeId, Employee.MvcClient.Models.Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employees/{EmployeeId}", employee);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return response.IsSuccessStatusCode;
        }


        // DELETE: api/Employees/{id}
        public async Task<bool> DeleteAsync(int EmployeeId)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/{EmployeeId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return response.IsSuccessStatusCode;
        }

    }
}
