using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeLogix.Client.Pages
{
    public partial class AddEditUser
    {
            [CascadingParameter] MudDialogInstance MudDialog { get; set; }

            [Parameter]
            public ApplicationUser entity
            {
                get { return _entity; }
                set
                {
                    _entity = value;
                }
            }

            private ApplicationUser _entity = new()
            {
            };


            private bool IsEdit => entity.Id != string.Empty;

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

