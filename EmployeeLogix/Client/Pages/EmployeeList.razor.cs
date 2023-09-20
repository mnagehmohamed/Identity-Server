using EmployeeLogix.Client.Services;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;

namespace EmployeeLogix.Client.Pages
{
    public partial class EmployeeList
    {
        #region Properties
        public List<Employee> AllEmployees = null;
        public Employee Employee { get; set; }= new();
        #endregion
        #region Injects
        [Inject]
        ISnackbar snackbar { get; set; }
        [Inject]
        IDialogService dialogService { get; set; }
        [Inject]
        public IEmployeeService employeeService { get; set; }
        #endregion
        #region Overrides
        protected  override async Task OnInitializedAsync()
        {
            
            await Task.Run(GetAllEmployees);
           
        }
        #endregion
  
        #region Actions
        public async Task GetAllEmployees()
        {
            System.Threading.Thread.Sleep(2000);
            AllEmployees = await employeeService.GetEmployees();
        }
        public async Task GetAllEmployeesForEditAndDelete()
        {
            
            AllEmployees = await employeeService.GetEmployees();
        }
        private async void UpdateTask(Employee employee)
        {
          
            var serverValue = await DoCheckExist(employee);
            // Check Existance
            if (serverValue == null)
            {
                snackbar.Add("Employee Not Found", Severity.Warning);
               await GetAllEmployees();
                return;
            }
            var parameters = new DialogParameters
            {

                ["entity"] = serverValue
            };
            DialogOptions dialogOptions = new DialogOptions()
            {
                MaxWidth = MaxWidth.Medium,
                CloseButton= true,
                Position=DialogPosition.Center
            };

            var dialog = dialogService.Show<EditEmployee>("Update", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                var sendValue = result.Data as Employee ;
                var status = await employeeService.UpdateEmployee(sendValue);
                await GetAllEmployeesForEditAndDelete();
                snackbar.Add($"Employee {employee.Name} uUpdated Successfully", Severity.Info);
                StateHasChanged();
            }

            }
        
        private async void DoDeleteAction(Employee employee)
        {

            DialogOptions dialogOptions = new DialogOptions();
            bool? result = await dialogService.ShowMessageBox("Warning",$"Are You Sure You Want To Delete Employee {employee.Name} ",yesText:"Delete",cancelText:"Cancel");
            if (result != null)
            {
                
                // Check Existance
                if (await DoCheckExist(employee) == null)
                {
                    snackbar.Add("Employee Not Found", Severity.Warning);
                    return;
                }
                await employeeService.DeleteEmployee(employee.Id);
                await GetAllEmployeesForEditAndDelete();
                StateHasChanged();
                snackbar.Add($"Employee {employee.Name} Deleted Successfully", Severity.Error);
            }

        }
        private async Task<Employee> DoCheckExist(Employee employee)
        {
            var result = await employeeService.GetEmployeeById(employee.Id);
            return result;
        }
        #endregion


    }
}
