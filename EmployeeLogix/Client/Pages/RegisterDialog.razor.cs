using EmployeeLogix.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeLogix.Client.Pages
{
    public partial class RegisterDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public RegistrationUser entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
            }
        }

        private RegistrationUser _entity = new()
        {
          

        };

        private void DoSubmit()
        {
            MudDialog.Close(DialogResult.Ok(entity));
        }
        private void DoCancel()
        {
            MudDialog.Cancel();
        }

    }
}

