using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Dynamic;
using System.Security.Claims;
using System.Net.Http.Headers;
using EmployeeLogix.Client.Services;

namespace EmployeeLogix.Client.Authentication
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymos;
        public AppAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _anonymos = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() 
        {
            if (await _localStorageService.GetItemAsync<string>("IsPresistentToken")!= "IsPresistent") 
            {
                await _localStorageService.RemoveItemAsync("AppToken");
            }
            var token = await _localStorageService.GetItemAsync<string>("AppToken");
            if (string.IsNullOrWhiteSpace(token))
                return _anonymos;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsfromjwt(token),"jwtAuthType")));
        }
        public async void NotifyUserAuthentication(string email) 
        {
            var authenticateduser = new ClaimsPrincipal(new ClaimsIdentity(new[] 
            {
            new Claim(ClaimTypes.Name,email)
            },"jwtAuthType"));
            var authstate = Task.FromResult(new AuthenticationState(authenticateduser));
            NotifyAuthenticationStateChanged(authstate);
        }
        public void NotifyUserLogout() 
        {
            var authState = Task.FromResult(_anonymos);
            NotifyAuthenticationStateChanged(authState);
        }
        //public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var myclaims = new List<Claim>
        //    {
        //    new Claim(ClaimTypes.Name,"Mohamed Nageh"),
        //    new Claim(ClaimTypes.Role,"Admin")
        //    };
        //  // var user = new ClaimsIdentity(myclaims,"Test");
        //   var user = new ClaimsIdentity();
      
        //    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));
        //}
    }
}
