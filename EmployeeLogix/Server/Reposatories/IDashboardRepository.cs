using EmployeeLogix.Shared.Models;

namespace EmployeeLogix.Server.Reposatories
{
    public interface IDashboardRepository
    {
        public Task<DashboardData> GetDashboardData(); 
    }
}
