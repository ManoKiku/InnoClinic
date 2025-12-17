using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Profiles.Application.Dto.Receptionist;

/// <summary>
/// Data transfer object for creating a new receptionist
/// </summary>
public class CreateReceptionistDto
{
    /// <summary>
    /// Receptionist's first name
    /// </summary>
    /// <example>John</example>
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    /// <summary>
    /// Receptionist's last name
    /// </summary>
    /// <example>Doe</example>
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    /// <summary>
    /// Receptionist's middle name (optional)
    /// </summary>
    /// <example>Michael</example>
    [MaxLength(50)]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Associated account ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// Office ID
    /// </summary>
    /// <example>98765432-4321-4321-4321-210987654321</example>
    [Required]
    public Guid OfficeId { get; set; }
}