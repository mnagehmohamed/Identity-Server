using EmployeeLogix.Server.AppDbContext;
using EmployeeLogix.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLogix.Server.Reposatories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly EmployeeContext _employeeContext;

        public DashboardRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<DashboardData> GetDashboardData()
        {
            var dashboard = new DashboardData() 
            {
            UsersActiveCount=await _employeeContext.Employees.CountAsync(x=>x.Status==true),
            UsersInActiveCount=await _employeeContext.Employees.CountAsync(y=>y.Status==false),
            };
            return dashboard;
        }
    }
}
