using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RevenueRecognitionSystem.Helpers;
using RevenueRecognitionSystem.Models.AuthRequests;
using RevenueRecognitionSystem.Models.Domain;
using RevenueRecognitionSystem.Services;
using LoginRequest = RevenueRecognitionSystem.Models.AuthRequests.LoginRequest;
using RegisterRequest = RevenueRecognitionSystem.Models.AuthRequests.RegisterRequest;

namespace RevenueRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration, IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(RegisterRequest model)
        {
            var employee = await authService.GetEmployeeByLogin(model.Login);
            if (employee != null)
            {
                return Conflict("User with this login already exists");
            }
            
            var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

            var newEmployee = new Employee()
            {
                Login = model.Login,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                Role = "Employee",
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1)
            };

            await authService.AddEmployee(newEmployee);
            
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var employee = await authService.GetEmployeeByLogin(loginRequest.Login);
            
            if (employee == null)
            {
                return Unauthorized();
            }

            string passwordHashFromDb = employee.Password;
            string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, employee.Salt);

            if (passwordHashFromDb != curHashedPassword)
            {
                return Unauthorized();
            }
            
            var employeeClaims = new[]
            {
                new Claim(ClaimTypes.Role, employee.Role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims: employeeClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            await authService.UpdateEmployeeRefreshToken(employee, DateTime.Now.AddDays(1));
            
            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = employee.RefreshToken
            });
        }
        
        [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
        {
            var employee  = await authService.GetEmployeeByRefreshToken(refreshToken.RefreshToken);
            if (employee == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            if (employee.RefreshTokenExp < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token expired");
            }
        
            var employeeClaim = new[]
            {
                new Claim(ClaimTypes.Role, employee.Role)
            };
            
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                claims: employeeClaim,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            
            await authService.UpdateEmployeeRefreshToken(employee, DateTime.Now.AddDays(1));
            
            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                refreshToken = employee.RefreshToken
            });
        }
    }
}
