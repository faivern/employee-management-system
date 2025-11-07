using System.Net.Http;
using System.Net.Http.Json;
using Employee.MvcClient.Models;

namespace Employee.MvcClient.Services
{
    public class DepartmentApiService
    {
        private readonly HttpClient _httpClient;

        public DepartmentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Departments
        public async Task<List<Department>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Departments");
            if (!response.IsSuccessStatusCode)
            {
                return new List<Department>();
            }

            var departments = await _httpClient.GetFromJsonAsync<List<Department>>("api/Departments");
            return departments ?? new List<Department>();
        }

        // GET: api/Departments/{id}
        public async Task<Department?> GetByIdAsync(int departmentId)
        {
            var response = await _httpClient.GetAsync($"api/Departments/{departmentId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var department = await _httpClient.GetFromJsonAsync<Department>($"api/Departments/{departmentId}");
            return department;
        }

        // POST: api/Departments
        public async Task<bool> CreateAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Departments", department);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/Departments/{id}
        public async Task<bool> UpdateAsync(int departmentId, Department department)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Departments/{departmentId}", department);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Departments/{id}
        public async Task<bool> DeleteAsync(int departmentId)
        {
            var response = await _httpClient.DeleteAsync($"api/Departments/{departmentId}");
            return response.IsSuccessStatusCode;
        }
    }
}
