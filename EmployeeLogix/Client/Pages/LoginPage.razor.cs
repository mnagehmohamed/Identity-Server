using EmployeeLogix.Client.Services;
using EmployeeLogix.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeLogix.Client.Pages
{
    public partial class LoginPage
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        public IAuthenticatedService authenticatedService { get; set; }
        public bool RegistrationError { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public LoginDTo entity { get; set; } = new();
        public bool Rememberme { get; set; }
        private async void DoLogin()
        {
            RegistrationError = false;
            entity.Rememeberme = Rememberme;
            var result = await authenticatedService.Login(entity);
            if (!result.isSuccessfull)
            {
                RegistrationError = true;
                Errors = result.errors;
            }
            else
            {
                //Console.WriteLine("Helooooooooooo");
                navigationManager.NavigateTo("/Dashboard");
            }
        }
        private void CheckRemember() 
        {
            Rememberme = !Rememberme;
        }
    }
}
