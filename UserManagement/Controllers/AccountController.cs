using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Entities;
using UserManagement.Model.Data;
using UserManagement.Model.Request;
using UserManagement.Model.Response;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ProductCategoryManagementContext _context;
        private readonly JwtSettings _jwtSettings;
        public AccountController(ProductCategoryManagementContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpGet("GetUsers")]
        public ActionResult<UserResponse> GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpPost("Login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return BadRequest(new LoginResponse
                {
                    IsSuccess = false,
                    Token = null
                });
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, request.Username!),
                new Claim(ClaimTypes.Role, request.Role!)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.DurationInMinutes)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    SecurityAlgorithms.HmacSha256)
                );

            var response = new LoginResponse
            {
                Username = request.Username!,
                IsSuccess = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return Ok(response);
        }
    }
}
