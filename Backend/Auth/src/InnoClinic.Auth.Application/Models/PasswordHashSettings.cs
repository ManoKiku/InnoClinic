namespace InnoClinic.Auth.Application.Models;

public class PasswordHashSettings
{
    public int SaltSize { get; set; } = 16;
    public int HashSize { get; set; } = 32;
    public int Iterations { get; set; } = 100000;
}