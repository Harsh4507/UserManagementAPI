using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Filters;
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
        [ServiceFilter(typeof(CustomActionFilter))]
        [ServiceFilter(typeof(CustomExceptionFilter))]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
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

        [HttpPost("LinqPractice")]
        public ActionResult<string> LinqPractice([FromBody] User request)
        {
            var list = new List<User>()
            {
                new User { Id = 1, Name = "John Doe", Age = 30, Salary = 50000, Gender = "male" },
                new User { Id = 2, Name = "Jane Smith", Age = 28, Salary = 60000, Gender = "memale" },
                new User { Id = 3, Name = "Mike Johnson", Age = 35, Salary = 70000, Gender = "male" },
                new User { Id = 4, Name = "Harsh", Age = 23, Salary = 35000, Gender = "male" },
                new User { Id = 5, Name = "Rhino", Age = 23, Salary = 35000, Gender = "female" },
                new User { Id = 6, Name = "Thakur", Age = 23, Salary = 47000, Gender = "female" },
            };

            var secondHighest = list.OrderByDescending(x => x.Salary).Skip(1).FirstOrDefault()!.Name;
            var list2 = list.Where(x => x.Age > 23).ToList();
            return "Login successful";
        }

        [HttpGet("GetUsers")]
        public ActionResult<string> GetUsers()
        {
            return "Login successful";
        }

        [HttpGet("GetUsers/{id}/{name}")]
        public ActionResult<string> GetUsers([FromRoute] int? id, [FromRoute] string? name)
        {
            return "Login successful";
        }

        [HttpGet("GetProduct")]
        public ActionResult<string> GetProduct([FromQuery] string? name, [FromQuery] string? id)
        {
            return "Login successful";
        }
    }
}
