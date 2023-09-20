using EmployeeLogix.Shared.Dtos;
using EmployeeLogix.Shared.Models;

namespace EmployeeLogix.Client.Services
{
    public interface IAuthenticatedService
    {
         Task<RegistrationResponse> Register(RegistrationUser registrationUser);
         Task<LoginResponse> Login(LoginDTo login);
         Task Logout();
        Task<ApplicationUser> Update(ApplicationUser user);
        Task Delete(string Id);
        Task<List<ApplicationUser>> GetList();
        Task<ApplicationUser> GetListByName(string Username);
        Task<ApplicationUser> GetListByEmail(string Email);
    }
}
