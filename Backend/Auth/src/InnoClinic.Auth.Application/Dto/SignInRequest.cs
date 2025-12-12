using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Auth.Application.Dto;

public class SignInRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [Length(6, 15)]
    public required string Password { get; set; }
}
