namespace UserManagement.Entities;

public partial class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Gender { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Password { get; set; } = null!;
}
