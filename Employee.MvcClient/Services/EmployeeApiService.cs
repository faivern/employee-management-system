using System.Net.Http;
using System.Net.Http.Json;
using Employee.MvcClient.Models;

namespace Employee.MvcClient.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmployeeApiService> _logger;

        public EmployeeApiService(HttpClient httpClient, ILogger<EmployeeApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Employee.MvcClient.Models.Employee>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all employees from API: {BaseAddress}api/Employees", _httpClient.BaseAddress);

                var response = await _httpClient.GetAsync("api/Employees");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to fetch employees. Status: {StatusCode}, Error: {Error}",
                        response.StatusCode, errorContent);
                    return new List<Employee.MvcClient.Models.Employee>();
                }

                var employees = await response.Content.ReadFromJsonAsync<List<Employee.MvcClient.Models.Employee>>();
                _logger.LogInformation("Successfully fetched {Count} employees", employees?.Count ?? 0);
                return employees ?? new List<Employee.MvcClient.Models.Employee>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request exception when fetching employees");
                return new List<Employee.MvcClient.Models.Employee>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when fetching employees");
                return new List<Employee.MvcClient.Models.Employee>();
            }
        }

    }
}
