using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Auth.Application.Dto;

public class SignUpRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [Length(6, 15)]
    public required string Password { get; set; }
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [Required]
    [Length(6, 15)]
    public required string PasswordConfirm { get; set; }
}
