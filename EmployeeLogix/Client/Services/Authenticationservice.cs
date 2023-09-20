using Blazored.LocalStorage;
using EmployeeLogix.Client.Authentication;
using EmployeeLogix.Shared.Dtos;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace EmployeeLogix.Client.Services
{
    public class Authenticationservice : IAuthenticatedService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly AuthenticationStateProvider _appAuthenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;
        public Authenticationservice(HttpClient httpClient, AuthenticationStateProvider appAuthenticationStateProvider, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _appAuthenticationStateProvider = appAuthenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task Delete(string Id)
        {
          await _httpClient.DeleteAsync($"api/Accounts/Delete?Id={Id}");
        }

        public async Task<List<ApplicationUser>> GetList()
        {
            return await _httpClient.GetFromJsonAsync<List<ApplicationUser>>("api/Accounts/GetList");
           
         }

        public async Task<ApplicationUser> GetListByEmail(string Email)
        {
            var result = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Accounts/GetListByEmail?Email={Email}");
            return result;
        }

        public async Task<ApplicationUser> GetListByName(string Username)
        {
            var result = await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Accounts/GetListByName?Username={Username}");
            return result;
        }

        public async Task<LoginResponse> Login(LoginDTo login)
        {
            var result = await _httpClient.PostAsJsonAsync<LoginDTo>("api/Accounts/Login", login);
            var resultmsg = await result.Content.ReadAsStringAsync();
            var msg = JsonSerializer.Deserialize<LoginResponse>(resultmsg, _jsonSerializerOptions);
            if (!result.IsSuccessStatusCode)
            {

                return msg;
            }

            await _localStorageService.SetItemAsync("AppToken",msg.Token);
            ((AppAuthenticationStateProvider)_appAuthenticationStateProvider).NotifyUserAuthentication(login.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer",msg.Token);
            if (login.Rememeberme)
            {
                await _localStorageService.SetItemAsync("IsPresistentToken", "IsPresistent");
            }
            else
                await _localStorageService.RemoveItemAsync("IsPresistentToken");
            return new LoginResponse {isSuccessfull=true };
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("AppToken");
            await _localStorageService.RemoveItemAsync("IsPresistentToken");
            ((AppAuthenticationStateProvider)_appAuthenticationStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponse> Register(RegistrationUser registrationUser)
        {
            var result = await _httpClient.PostAsJsonAsync<RegistrationUser>("api/Accounts/Register", registrationUser);
            if (!result.IsSuccessStatusCode)
            {
            var resultmsg=await result.Content.ReadAsStringAsync();
                var msg=JsonSerializer.Deserialize<RegistrationResponse>(resultmsg,_jsonSerializerOptions);
                return msg;
            }
            return new RegistrationResponse {isSuccessfull=true };
        }

        public async Task<ApplicationUser> Update(ApplicationUser user)
        {
            var result = await _httpClient.PutAsJsonAsync<ApplicationUser>("api/Accounts/Update", user);
            return await result.Content.ReadFromJsonAsync<ApplicationUser>();
        }
    }
}
