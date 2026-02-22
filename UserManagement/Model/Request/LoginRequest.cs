using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model.Request
{
    public class LoginRequest
    {
        [Required]
        public string? Username { get; set; }
        //public readonly string Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public string? Role { get; set; }
        //public readonly string Password { get; set; }
    }
}
