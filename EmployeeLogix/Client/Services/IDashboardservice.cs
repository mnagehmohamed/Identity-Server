using EmployeeLogix.Shared.Models;

namespace EmployeeLogix.Client.Services
{
    public interface IDashboardservice
    {
        Task<DashboardData> GetDashboardData();
    }
}
