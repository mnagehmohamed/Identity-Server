using EmployeeLogix.Shared.Models;
using System.Net.Http.Json;

namespace EmployeeLogix.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteEmployee(Guid Id)
        {
            await _httpClient.DeleteAsync($"api/Employee/DeleteEmployee/{Id}");
        }

        public async Task<Employee> GetEmployeeById(Guid Id)
        {
         var employee=   await _httpClient.GetFromJsonAsync<Employee>($"api/Employee/GetEmployeeById/{Id}");
            return employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>($"api/Employee/GetAllData");
            return employees;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
           var result= await _httpClient.PutAsJsonAsync($"api/Employee/EditEmployee",employee);
            return await result.Content.ReadFromJsonAsync<Employee>();
          
        }
    }
}
