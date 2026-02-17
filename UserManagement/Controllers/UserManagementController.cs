using Microsoft.AspNetCore.Mvc;
using UserManagement.Model.Request;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] LoginRequest request)
        {
            return "Login successful";
        }
    }
}
