using EmployeeLogix.Shared.Models;
using System.Net.Http.Json;

namespace EmployeeLogix.Client.Services
{
    public class DashboardService : IDashboardservice
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardData> GetDashboardData()
        {
            return await _httpClient.GetFromJsonAsync<DashboardData>("api/Dasboard/GetDashboardData");
        }
    }
}
