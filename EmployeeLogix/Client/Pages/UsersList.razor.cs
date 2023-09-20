using EmployeeLogix.Client.Services;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace EmployeeLogix.Client.Pages
{
    public partial class UsersList
    {
        #region Properties
        [Inject] public IAuthenticatedService authenticatedService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        ISnackbar snackbar { get; set; }
        [Inject]
        IDialogService dialogService { get; set; }
        public List<ApplicationUser> users = new();

        #endregion
        #region Overrides
        protected async override Task OnInitializedAsync()
        {

            await LoadDataList();
            await base.OnInitializedAsync();
        }
        #endregion


     
       
        #region Data Actions

        public async Task LoadDataList()
        {
            users = await authenticatedService.GetList();
            StateHasChanged();
        }

        private async void DoTableSearch(string text)
        {
          //  await Task.Delay(1);
          //  if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
          //  {
          //      users = await authenticatedService.GetList();
          //      StateHasChanged();
          //      return;
          //  }
          //users=await authenticatedService.GetListByEmail(text);
          //  StateHasChanged();
        }
        private async void DoUpdateActions(ApplicationUser user)
        {
            var serverValue = await DoCheckExist(user);
            // Check Existance
            if (serverValue == null)
            {
                snackbar.Add("Data Refreshed", Severity.Info);
                await LoadDataList();
                return;
            }
            var parameters = new DialogParameters
            {

                ["entity"] = serverValue,
            };
            var dialogOptions = new DialogOptions()
            {
                FullWidth = true,
                Position = DialogPosition.Center,
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Small,
                ClassBackground = "blurred-dialog"
            };
            dialogOptions.MaxWidth = MaxWidth.Medium;
            var dialog = dialogService.Show<AddEditUser>("Update", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                var sendValue = result.Data as ApplicationUser;
                var status = await authenticatedService.Update(sendValue);

                await LoadDataList();
                snackbar.Add($"User {user.UserName} Updated Successfully", Severity.Info);
            }
        }
        private async void DoInfoAction(ApplicationUser entity)
        {
            var serverValue = await DoCheckExist(entity);
            // Check Existance
            if (serverValue == null)
            {
                snackbar.Add("Data Refreshed",Severity.Info);
                await LoadDataList();
                return;
            }
            var parameters = new DialogParameters
            {
                ["entity"] = serverValue
            };
            var dialogOptions = new DialogOptions() 
            {
                FullWidth = true,
                Position = DialogPosition.Center,
                CloseButton = true,
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.Small,
                ClassBackground = "blurred-dialog"
            };
            dialogOptions.MaxWidth = MaxWidth.Medium;
            var dialog = dialogService.Show<InfoUser>("", parameters, dialogOptions);

        }
        private async Task<ApplicationUser> DoCheckExist(ApplicationUser users)
        {
            var result = await authenticatedService.GetListByEmail(users.Email);
            return result;
        }


        private async void DoDeleteAction(ApplicationUser user)
        {

            bool? result = await dialogService.ShowMessageBox(
           "Warning",
           $"Are You Sure You To Delete User {user.UserName}",
             yesText: "Yes", cancelText: "No");
            if (result != null)
            {

                // Check Existance
                if (await DoCheckExist(user) == null)
                {
                    snackbar.Add("Data Refreshed", Severity.Info);
                    await LoadDataList();
                    return;
                }
                    await authenticatedService.Delete(user.Id);
                    await LoadDataList();
                snackbar.Add($"User {user.UserName} Deleted Successfully",Severity.Error);

            }

        }

        #endregion

    }
}
