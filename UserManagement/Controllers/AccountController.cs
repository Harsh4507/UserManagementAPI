using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Entities;
using UserManagement.Model.Response;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ProductCategoryManagementContext _context;
        public AccountController(ProductCategoryManagementContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public ActionResult<UserResponse> GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
