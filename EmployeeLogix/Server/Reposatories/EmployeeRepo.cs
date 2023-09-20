using EmployeeLogix.Server.AppDbContext;
using EmployeeLogix.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLogix.Server.Reposatories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepo(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task DeleteEmployeeById(Guid Id)
        {
            var Employee = await FindEmployeeById(Id);
            _employeeContext.Employees.Remove(Employee);
            _employeeContext.SaveChanges();
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            var Employee = await FindEmployeeById(employee.Id);
            Employee.Id = employee.Id;
            Employee.Name = employee.Name;
            Employee.Email = employee.Email;
            Employee.Salary = employee.Salary;
            Employee.Status = employee.Status;
            Employee.BirthDate = employee.BirthDate;
            _employeeContext.SaveChanges();
            return Employee;
        }

        public async Task<Employee> FindEmployeeById(Guid Id)
        {
            var Employee = await _employeeContext.Employees.FindAsync(Id);
            return Employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var Employees = await _employeeContext.Employees.OrderBy(n=>n.Name).ToListAsync();
            return Employees;
        }
    }
}
