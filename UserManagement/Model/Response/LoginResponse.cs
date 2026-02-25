namespace UserManagement.Model.Response
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public bool IsSuccess { get; set; }
        public string? Token { get; set; }
    }
}
