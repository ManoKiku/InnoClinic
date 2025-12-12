namespace InnoClinic.Auth.Application.Models;

public class AuthSettings
{
    public bool RequireEmailVerification { get; set; } = true;
    public int MaxFailedAttempts { get; set; } = 5;
    public int LockoutMinutes { get; set; } = 15;
}