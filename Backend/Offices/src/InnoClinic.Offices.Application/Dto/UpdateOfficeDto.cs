using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Offices.Application.Dto;

/// <summary>
/// DTO for updating an existing office
/// </summary>
public class UpdateOfficeDto
{
    /// <summary>
    /// Office address
    /// <example>Pushkin street, 25</example>
    /// </summary>
    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
    public string Address { get; set; }
        
    /// <summary>
    /// Photo identifier
    /// <example>223e4567-e89b-12d3-a456-426614174001</example>
    /// </summary>
    public Guid? PhotoId { get; set; }
        
    /// <summary>
    /// Registry phone number
    /// <example>+375 29 539 21 21</example>
    /// </summary>
    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string RegistryPhoneNumber { get; set; }
        
    /// <summary>
    /// Office activity status
    /// <example>true</example>
    /// </summary>
    public bool? IsActive { get; set; }
}