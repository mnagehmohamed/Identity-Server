﻿using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeLogix.Client.Pages
{
    public partial class InfoUser
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }


        [Parameter]
        public ApplicationUser entity { get; set; } = new ();

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
