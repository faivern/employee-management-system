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
            var response = await _httpClient.GetAsync("api/Employees");

            if (!response.IsSuccessStatusCode)
            {
                // logga, visa tom lista, kasta eget fel, etc
                return new List<Employee.MvcClient.Models.Employee>();
            }

            var employees = await response.Content.ReadFromJsonAsync<List<Employee.MvcClient.Models.Employee>>();
            return employees ?? new List<Employee.MvcClient.Models.Employee>();
        }

        public async Task<Employee.MvcClient.Models.Employee?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/{id}");
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Employee.MvcClient.Models.Employee>();
        }

        public async Task<bool> CreateAsync(Employee.MvcClient.Models.Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Employees", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, Employee.MvcClient.Models.Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Employees/{id}", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
