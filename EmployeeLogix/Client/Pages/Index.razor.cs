using EmployeeLogix.Client.Services;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace EmployeeLogix.Client.Pages
{
    public partial class Index
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IDashboardservice dashboardservice { get; set; }
        public DashboardData dashboarddata { get; set; } = new();
        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            dashboarddata = await dashboardservice.GetDashboardData();
        }
        #endregion
        public void GoToEmployee() 
        {
            navigationManager.NavigateTo("EmployeePage");
        }
    }
}
