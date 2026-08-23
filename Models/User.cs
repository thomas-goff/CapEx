namespace CapEx.Models;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();

    public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
}
