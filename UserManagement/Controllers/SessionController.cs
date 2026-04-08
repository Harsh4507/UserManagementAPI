using Microsoft.AspNetCore.Mvc;
using UserManagement.Model.Request;
using Newtonsoft.Json;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private const string sessionKey = "UserSession";
        
        [HttpPost("CreateUsers")]
        public IActionResult CreateUsers([FromBody] List<SessionList> request) 
        {
            HttpContext.Session.SetString(sessionKey, JsonConvert.SerializeObject(request));
            return Ok(request);
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var response = HttpContext.Session.GetString(sessionKey);
            var users = new List<SessionList>();
            if (response != null) {
                users = JsonConvert.DeserializeObject<List<SessionList>>(response);
            }
            return Ok(users);
        }
    }
}
