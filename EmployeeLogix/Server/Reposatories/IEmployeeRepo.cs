using EmployeeLogix.Shared.Models;

namespace EmployeeLogix.Server.Reposatories
{
    public interface IEmployeeRepo
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> EditEmployee(Employee employee);
        public Task<Employee> FindEmployeeById(Guid Id);
        public Task DeleteEmployeeById(Guid Id);
    }
}
