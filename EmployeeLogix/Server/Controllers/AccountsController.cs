using EmployeeLogix.Shared.Dtos;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeLogix.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _configurationSection;
        public AccountsController(UserManager<ApplicationUser> usermanager, IConfiguration configuration)
        {
            _usermanager = usermanager;
            _configuration = configuration;
            _configurationSection = _configuration.GetSection("JWT");
        }
        [HttpGet]
        public async Task<IActionResult> GetList() 
        {
            var users =await  _usermanager.Users.ToListAsync();
            if (users == null)
            {
                return BadRequest("Users Not Founs");
            }
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetListByName(string Username)
        {
            var users =await _usermanager.FindByNameAsync(Username);
            if (users == null)
            {
                return BadRequest("User Not Founs");
            }
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetListByEmail(string Email)
        {
            var users = await _usermanager.FindByEmailAsync(Email);
            if (users == null)
            {
                return BadRequest("User Not Founs");
            }
            return Ok(users);
        }
        [HttpPost]

        public async Task<IActionResult> Register([FromBody] RegistrationUser registrationUser)
        {

            if (registrationUser == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                UserName = registrationUser.UserName,
                Email = registrationUser.Email,
            };
            var result = await _usermanager.CreateAsync(user, registrationUser.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { errors = errors });
            }
            return StatusCode(201);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string Id) 
        {
            var user =await _usermanager.FindByIdAsync(Id);
            if (user==null) 
            {
                return BadRequest("User Not Found");
            }
            await _usermanager.DeleteAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ApplicationUser user)
        {
            if (user == null) return BadRequest("Null User");
             var Updateduser = await _usermanager.FindByIdAsync(user.Id);
            if (Updateduser == null)
            {
                return BadRequest("User Not Found");
            }
            Updateduser.Id = user.Id;
            Updateduser.Country = user.Country;
            Updateduser.Address = user.Address;
            Updateduser.UserName = user.UserName;
            Updateduser.NormalizedUserName = user.NormalizedUserName;
            Updateduser.Email = user.Email;
            Updateduser.NormalizedEmail = user.NormalizedEmail;
            Updateduser.PasswordHash = user.PasswordHash;
            Updateduser.SecurityStamp = user.SecurityStamp;
            Updateduser.PhoneNumber = user.PhoneNumber;
            Updateduser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            await _usermanager.UpdateAsync(Updateduser);
            return Ok(Updateduser);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTo loginuser)
        {
            var user = await _usermanager.FindByNameAsync(loginuser.UserName);
            if (user == null)
            {
                user = await _usermanager.FindByEmailAsync(loginuser.UserName);
            }
            if (user == null || !await _usermanager.CheckPasswordAsync(user, loginuser.Password))
            {
                return Unauthorized(new LoginResponse { errors = new[] { "Failed To Login" } });
            }
            var signinCredentials = GetSigningCredentials();
            var Claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signinCredentials, Claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new LoginResponse { isSuccessfull = true, Token = token });
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configurationSection["SecurityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName)
    };

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _configurationSection["Issure"],
                audience: _configurationSection["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configurationSection["expireMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
