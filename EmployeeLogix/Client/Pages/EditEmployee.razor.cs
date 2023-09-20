using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;

namespace EmployeeLogix.Client.Pages
{
    public partial class EditEmployee
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Inject]
        ISnackbar snackbar { get; set; }

        private Employee _entity = new()
        {
            Id = Guid.Empty,
        };

        [Parameter]
        public Employee entity
        {
            get { return _entity; }
            set
            {

                _entity = value;

            }
        }
        private async Task DoSubmit()
        {
            if ( string.IsNullOrEmpty(entity.Name))
                 snackbar.Add("",Severity.Warning);
            if (string.IsNullOrEmpty(entity.Email))
                snackbar.Add("Please Insert Email", Severity.Warning);
            if (string.IsNullOrEmpty(entity.Salary.ToString()))
                snackbar.Add("Please Insert Salary", Severity.Warning);
            MudDialog.Close(DialogResult.Ok(entity));
        }


        private void DoCancel()
        {
            MudDialog.Cancel();
        }
    }
}
