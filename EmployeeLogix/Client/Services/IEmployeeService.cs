using EmployeeLogix.Shared.Models;

namespace EmployeeLogix.Client.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(Guid id);
        Task<Employee> UpdateEmployee (Employee employee);
        Task DeleteEmployee(Guid Id);
    }
}
