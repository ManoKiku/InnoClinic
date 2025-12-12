using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Auth.Application.Dto;

public class UpdateAccountDto
{
    [Phone]
    public string? PhoneNumber { get; set; }
    public Guid? PhotoId { get; set; }
}