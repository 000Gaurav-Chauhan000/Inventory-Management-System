namespace AuthService.Entities;

public class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? Phone { get; set; } 

    public string Role { get; set; } = "STAFF";  

    public string Department { get; set; } = "General";  

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }
}