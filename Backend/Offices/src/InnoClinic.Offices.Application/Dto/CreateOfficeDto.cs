using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Offices.Application.Dto;

/// <summary>
/// DTO for creating a new office
/// </summary>
public class CreateOfficeDto
{
    /// <summary>
    /// Office address
    /// <example>Pushkin street, 25</example>
    /// </summary>
    [Required(ErrorMessage = "Address is required")]
    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
    public string Address { get; set; }
        
    /// <summary>
    /// Photo identifier
    /// <example>223e4567-e89b-12d3-a456-426614174001</example>
    /// </summary>
    [Required(ErrorMessage = "Photo identifier is required")]
    public Guid PhotoId { get; set; }
        
    /// <summary>
    /// Registry phone number
    /// <example>+375 29 539 21 21</example>
    /// </summary>
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string RegistryPhoneNumber { get; set; }
        
    /// <summary>
    /// Office activity status
    /// <example>true</example>
    /// </summary>
    public bool IsActive { get; set; } = true;
}