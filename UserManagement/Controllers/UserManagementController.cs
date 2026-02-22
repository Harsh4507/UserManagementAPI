using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Model.Data;
using UserManagement.Model.Request;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public UserManagementController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Login")]
        [Authorize]
        public ActionResult<string> Login([FromBody] LoginRequest request)
        {
            return "Login successful";
        }

        [HttpPost("AdminLogin")]
        [Authorize(Roles ="Admin")]
        public ActionResult<string> AdminLogin([FromBody] LoginRequest request)
        {
            return "Login successful";
        }

        [HttpPost("PartnerLogin")]
        [Authorize(Roles = "Partner")]
        public ActionResult<string> PartnerLogin([FromBody] LoginRequest request)
        {
            return "Login successful";
        }

        [HttpPost("JwtLogin")]
        public IActionResult JwtLogin([FromBody] LoginRequest request)
        {
            var claims = new[] { 
                new Claim(ClaimTypes.Name, request.Username!),
                new Claim(ClaimTypes.Role, request.Role!)
            };

            var token = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.DurationInMinutes)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                    SecurityAlgorithms.HmacSha256)
                );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
